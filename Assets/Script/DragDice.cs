using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDice : MonoBehaviour
{
    // 드래그가 가능한 상태인지 확인하는 bool 변수
    public bool draggable;

    public bool isSeat;

    // 매 프레임 별 호출되는 메소드
    void Update()
    {
        // 사용자가 마우스 왼쪽버튼을 눌렀을 때 true를 반환해 if문 내 코드 실행
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast를 생성해 부딪힌 오브젝트 반환
            RaycastHit hit = CastRay();

            if (!hit.transform.CompareTag("Dice"))
                return;

            if(!hit.transform.GetComponent<DiceRoll>().GetRollState())
            {
                GameManager.GetInstance.Menu.OpenAlert("주사위를 굴려야 \n움직일 수 있습니다.");
                return;
            }

            if (hit.transform.GetComponent<DragDice>().isSeat)
            {
                Debug.Log("완료된 주사위입니다");
                return;
            }

            // 부딪힌 오브젝트가 현재 스크립트 update메소드가 실행되고 있는 오브젝트일 경우
            if (hit.transform == transform)
            {
                // 드래그가 가능한 상태로 변경
                draggable = true;
                SortingRot();
                GetComponent<Rigidbody>().freezeRotation = true;
                GetComponent<BoxCollider>().isTrigger = true;
            }
        }

        // 사용자가 마우스 왼쪽버튼을 누른 상태에서 손가락을 떼었을 때 true를 반환해 if문 내 코드 실행
        if (Input.GetMouseButtonUp(0))
        {
            // 드래그가 불가능한 상태로 변경
            draggable = false;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<BoxCollider>().isTrigger = false;
        }

        // 현재 드래그가 가능한 상태일 경우 if문 내 코드 실행
        if (draggable)
        {
            // 현재 화면에 있는 마우스 커서의 x,y 좌표와 카메라를 통해 보는 이 스크립트가 실행되는 오브젝트의 z좌표를 사용해 ScreenPoint Vector3 position 값 생성
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            // 오브젝트를 이동할 때 움직일 x,z 좌표를 가진 WorldPoint Vector3 position 생성
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            // 위 worldPosition의 x,z 좌표를 사용하고 직접 y좌표를 설정해 오브젝트 이동
            transform.position = new Vector3(worldPosition.x, 2.5f, worldPosition.z);
        }
    }

    // Raycast에 충돌한 정보를 반환하는 메소드
    private RaycastHit CastRay()
    {
        // 마우스 커서가 가리키는 카메라가 랜더링하는 가장 먼곳의 위치 ScreenPoint Vector3 position
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        // 마우스 커서가 가리키는 카메라가 랜더링하는 가장 가까운곳의 위치 ScreenPoint Vector3 position
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);

        // 각 위치를 WorldPosition으로 변경
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        // RaycastHit 정보를 담을 변수 생성
        RaycastHit hit;
        // 현재 worldMousePosNear에서 시작하고 worldMousePosFar로 향하는 Raycast를 생성한다
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        // 정보를 가진 hit 반환
        return hit;
    }

    private void SortingRot()
    {
        //1, 6 = X
        //2, 5 = Y
        //3, 4 = Z

        int val = GetComponent<DiceRoll>().GetDiceEye();

        switch(val)
        {
            case 1:
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 0);
                break;
            case 2:
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z);
                break;
            case 3:
                transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z);
                break;
            case 4:
                transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z);
                break;
            case 5:
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z);
                break;
            case 6:
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 0);
                break;
        }
       
    }


}
