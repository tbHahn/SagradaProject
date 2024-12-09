using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDice : MonoBehaviour
{
    // �巡�װ� ������ �������� Ȯ���ϴ� bool ����
    public bool draggable;

    public bool isSeat;

    // �� ������ �� ȣ��Ǵ� �޼ҵ�
    void Update()
    {
        // ����ڰ� ���콺 ���ʹ�ư�� ������ �� true�� ��ȯ�� if�� �� �ڵ� ����
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast�� ������ �ε��� ������Ʈ ��ȯ
            RaycastHit hit = CastRay();

            if (!hit.transform.CompareTag("Dice"))
                return;

            if(!hit.transform.GetComponent<DiceRoll>().GetRollState())
            {
                GameManager.GetInstance.Menu.OpenAlert("�ֻ����� ������ \n������ �� �ֽ��ϴ�.");
                return;
            }

            if (hit.transform.GetComponent<DragDice>().isSeat)
            {
                Debug.Log("�Ϸ�� �ֻ����Դϴ�");
                return;
            }

            // �ε��� ������Ʈ�� ���� ��ũ��Ʈ update�޼ҵ尡 ����ǰ� �ִ� ������Ʈ�� ���
            if (hit.transform == transform)
            {
                // �巡�װ� ������ ���·� ����
                draggable = true;
                SortingRot();
                GetComponent<Rigidbody>().freezeRotation = true;
                GetComponent<BoxCollider>().isTrigger = true;
            }
        }

        // ����ڰ� ���콺 ���ʹ�ư�� ���� ���¿��� �հ����� ������ �� true�� ��ȯ�� if�� �� �ڵ� ����
        if (Input.GetMouseButtonUp(0))
        {
            // �巡�װ� �Ұ����� ���·� ����
            draggable = false;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<BoxCollider>().isTrigger = false;
        }

        // ���� �巡�װ� ������ ������ ��� if�� �� �ڵ� ����
        if (draggable)
        {
            // ���� ȭ�鿡 �ִ� ���콺 Ŀ���� x,y ��ǥ�� ī�޶� ���� ���� �� ��ũ��Ʈ�� ����Ǵ� ������Ʈ�� z��ǥ�� ����� ScreenPoint Vector3 position �� ����
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            // ������Ʈ�� �̵��� �� ������ x,z ��ǥ�� ���� WorldPoint Vector3 position ����
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            // �� worldPosition�� x,z ��ǥ�� ����ϰ� ���� y��ǥ�� ������ ������Ʈ �̵�
            transform.position = new Vector3(worldPosition.x, 2.5f, worldPosition.z);
        }
    }

    // Raycast�� �浹�� ������ ��ȯ�ϴ� �޼ҵ�
    private RaycastHit CastRay()
    {
        // ���콺 Ŀ���� ����Ű�� ī�޶� �������ϴ� ���� �հ��� ��ġ ScreenPoint Vector3 position
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        // ���콺 Ŀ���� ����Ű�� ī�޶� �������ϴ� ���� �������� ��ġ ScreenPoint Vector3 position
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);

        // �� ��ġ�� WorldPosition���� ����
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        // RaycastHit ������ ���� ���� ����
        RaycastHit hit;
        // ���� worldMousePosNear���� �����ϰ� worldMousePosFar�� ���ϴ� Raycast�� �����Ѵ�
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        // ������ ���� hit ��ȯ
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
