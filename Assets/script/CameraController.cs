using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;    //�⺻���� BackView

    [SerializeField]
    public Transform _target;          //Ÿ�ٵ��
    [SerializeField]
    public Transform cameraArm;        //ī�޶� �� ���
    [SerializeField]
    Vector3 cameraPos = new Vector3(0.0f, 10f, -8.0f);  //�⺻ ī�޶� ��ġ


    private float scrollSpeed = 5.0f;
    private float minScroll = 20.0f;
    private float maxScroll = 80.0f;

    void Start()
    {

    }

    void LateUpdate()
    {
        Camera.main.fieldOfView += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;   //ī�޶� ���� �ܾƿ�
        if (Camera.main.fieldOfView < minScroll)
        {
            Camera.main.fieldOfView = minScroll;
        }
        else if (Camera.main.fieldOfView > maxScroll)
        {
            Camera.main.fieldOfView = maxScroll;
        }

        if (Input.GetKeyDown(KeyCode.Tab))   //�Ǵ����� ȭ���� ��ȯ
        {
            switch (_mode)
            {
                case Define.CameraMode.BackView:
                    _mode++;
                    break;
                case Define.CameraMode.QuarterView:
                    _mode--;
                    break;
            }
        }

        switch (_mode)  //ī�޶� ��忡���� �����Լ� ����
        {
            case Define.CameraMode.BackView:
                LookAround();
                break;
            case Define.CameraMode.QuarterView:
                LookQuarterView();
                break;
        }
        if (_mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hit;
            Ray ray = new Ray();
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);
            if (Physics.Raycast(_target.transform.position, cameraPos, out hit, cameraPos.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _target.transform.position).magnitude * 0.8f;
                transform.position = _target.transform.position + cameraPos.normalized * dist;
            }
            else
            {
                transform.LookAt(_target.transform);
            }
        }
    }

    private void LookAround()
    {
        cameraArm.transform.position = new Vector3(0f, 2f, -3f) + _target.position; //ī�޶�� �⺻ ��ġ����
        cameraArm.rotation = Quaternion.Euler(0f, 0f, 0f);
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));   //(�ӽ�) ���콺�Է� Vector2�� �޾ƿ���
        Vector3 camAngle = cameraArm.rotation.eulerAngles;  //ī�޶� ���� Rotate��

        float x = camAngle.x - mouseDelta.y;    // ī�޶� ����ȸ�� ���ѱ��ذ�

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -40f, 60f);  // ���ʹ��� rotate ����
        }
        else
        {
            x = Mathf.Clamp(x, 300f, 361f); // �Ʒ����� rotate ����
        }

        if (Input.GetMouseButton(1)) //���콺 ���� ��ư�� ����������, ī�޶� ȸ������
        {
            cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        }
    }

    private void LookQuarterView()
    {
        cameraArm.transform.position = new Vector3(0.0f, 10.0f, -8.0f) + _target.position;   // ���ͺ� ���� �ʱⰪ = �÷��̾� ��ǥ + ī�޶� ���� ���Ͱ�
        cameraArm.rotation = Quaternion.Euler(40f, 0f, 0f); // ī�޶� ȸ����, �Ʒ����� 40�� ȸ������
    }
}
