using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    public GameObject teleport_pointer;
    public SteamVR_Action_Boolean Teleport_Action;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool HasPosition = false;
    private bool m_isTeleporting = false;
    private float m_Fadetime = 0.05f;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        //pointer 
        HasPosition = UpdatePointer();
        //make pointer visible 
        teleport_pointer.SetActive(HasPosition);

        //teleport
        if (Teleport_Action.GetStateUp(m_Pose.inputSource))
        {
            TryTeleport();
        }
    }

    private void TryTeleport()
    {
        //check valid position, if alrady teleport 
        if (!HasPosition || m_isTeleporting)
        {
            return;
        }

        //get camera rig , head to position 
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        //figure out translation 
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = teleport_pointer.transform.position - groundPosition;

        //move 
        StartCoroutine(MoveRig(cameraRig, translateVector));


    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        //flag
        m_isTeleporting = true;

        //fade to black 
        SteamVR_Fade.Start(Color.black, m_Fadetime, true);

        //apply translation
        yield return new WaitForSeconds(m_Fadetime);
        cameraRig.position += translation;

        //fade to clear 
        SteamVR_Fade.Start(Color.clear, m_Fadetime, true);

        //deflag boolean 
        m_isTeleporting = false;

        yield return null;
    }

    private bool UpdatePointer()
    {
        //ray coming from controller 
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //if hits 
        if (Physics.Raycast(ray, out hit))
        {
            teleport_pointer.transform.position = hit.point;
            return true; 
        }

        //if not hits 
        return false;
    }

}
