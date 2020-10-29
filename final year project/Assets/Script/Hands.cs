using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hands : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose= null;
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContactInteractable = new List<Interactable>();

    //teleporting enable unable 
    public GameObject controller;
    public GameObject controllerPointer;

    //pointer 
    public GameObject pointerLaser;


    private void Awake()
    {
        //setting to get input from the controller 
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();

        //laser pointer 
        pointerLaser.SetActive(false);

        //when start, player cannot teleport yet 
        controller.GetComponent<Teleporter>().enabled = false;
    }

    //calling teleport class, set teleport pointer to true 
    //if can teleport 
    public void Teleport()
    {
        controller.GetComponent<Teleporter>().enabled = true;
        controllerPointer.SetActive(true);        
    }

    //if cannot teleport, hide controller pointer 
    public void CantTeleport()
    {
        controller.GetComponent<Teleporter>().enabled = false;
        controllerPointer.SetActive(false);
    }

    public void LaserOut()
    {
        pointerLaser.SetActive(false);
    }

    public void LeaveRoom()
    {
        pointerLaser.SetActive(true);
    }


    //codes for grabbing object
    private void Update()
    {
        //Down 
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            //if user pressed the triggered 
            print(m_Pose.inputSource + "Trigger Down!");
            PickUp();
        }
        //Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            //if user release the triggered 
            print(m_Pose.inputSource + "Trigger Up!");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //checking the object tag 
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        //add the gameobject to the list 
        m_ContactInteractable.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        //checking the object tag 
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        //add the gameobject to the list 
        m_ContactInteractable.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void PickUp()
    {
        //get the nearest interactable  
        m_CurrentInteractable = GetNearestInteractable();

        //check if we have something to pick up 
        if (!m_CurrentInteractable)
            //fasle 
            return;

        //check if we are held something 
        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();

        //postion to the controller 
        //set this to our current positon of the hands 
        m_CurrentInteractable.transform.position = transform.position;

        //attach object rigidbody to both hands 
        Rigidbody targetbody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetbody;
        //connecting rigidbody from controller to rigidbody for object 

        //set active hands 
        m_CurrentInteractable.m_ActiveHand = this;
    }

    public void Drop()
    {
        //null check 
        if (!m_CurrentInteractable)
            //fasle 
            return;

        //apply velocity to drop object 
        Rigidbody targetbody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetbody.velocity = m_Pose.GetVelocity();
        targetbody.angularVelocity = m_Pose.GetAngularVelocity();

        //detached from controller 
        m_Joint.connectedBody = null;

        //clear all the variable 
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    //to get the nearest object to interact with 
    private Interactable GetNearestInteractable()
    {
        //store nearest interactable 
        Interactable nearest = null;
        //check distance
        float min_distance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interactable interactable in m_ContactInteractable)
        {
            //check distance of interactable with hands 
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < min_distance)
            {
                min_distance = distance;
                nearest = interactable;
            }
        }


        return nearest;
    }

}
