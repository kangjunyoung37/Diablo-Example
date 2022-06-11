using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipMentCombiner 
{
    private readonly Dictionary<int , Transform> rootBoneDictionary = new Dictionary<int, Transform>();

    private readonly Transform transform;

    public EquipMentCombiner(GameObject rootGO)
    {
        transform = rootGO.transform;
        TraverseHierachy(transform);
       
    }

    public Transform AddLimb(GameObject itemGO, List<string> boneNames)
    {
        Transform limb = ProcessBoneObject(itemGO.GetComponentInChildren<SkinnedMeshRenderer>(), boneNames);//item������Ʈ�� ��Ų�������� ��������
        limb.SetParent(transform);//���� Ʈ�������� �θ�� ����
        return limb;
    }

    private Transform ProcessBoneObject(SkinnedMeshRenderer renderer, List<string> boneNames)
    {
        //�⺻ ��Ų��޽��� ���縦 �ؼ� ������Ʈ�� �߰��ϰ� �ڽ����� �߰�
        Transform itemTransform = new GameObject().transform;
        SkinnedMeshRenderer meshRenderer = itemTransform.gameObject.AddComponent<SkinnedMeshRenderer>();
        Transform[] boneTransforms = new Transform[boneNames.Count];
        for (int i = 0; i < boneNames.Count; i++)
        {
            boneTransforms[i] = rootBoneDictionary[boneNames[i].GetHashCode()];
        }
        meshRenderer.bones = boneTransforms;
        meshRenderer.sharedMesh = renderer.sharedMesh;
        meshRenderer.materials = renderer.sharedMaterials;
        return itemTransform;
        

    }


    //���ⰰ�� staticmesh�� ������ �� ���
    public Transform[] AddMesh(GameObject itemGO)
    {
        Transform[] itemTransforms = ProcessMeshObject(itemGO.GetComponentsInChildren<MeshRenderer>());
        return itemTransforms;
    }

    private Transform[] ProcessMeshObject(MeshRenderer[] meshRenderes)
    {
        List<Transform> itemstransforms = new List<Transform>();
        foreach(MeshRenderer renderer in meshRenderes)
        {
            if (renderer.transform.parent != null)
            {
                Transform parent = rootBoneDictionary[renderer.transform.parent.GetHashCode()];

                GameObject itemGO = GameObject.Instantiate(renderer.gameObject, parent);
                itemstransforms.Add(itemGO.transform);
            }
        }
        return itemstransforms.ToArray();
    }

    private void TraverseHierachy(Transform root)
    {
        foreach(Transform child in root)
        {
            rootBoneDictionary.Add(child.name.GetHashCode(), child);//GetHash ��Ʈ���� int������ ����ȯ���Ѽ� ����
            //��Ʈ��Ÿ�Գ��� �񱳸� �Ҷ��� ��Ʈ������ ��ȯ�ؼ� ���ϴ°� �ð��� ����
            TraverseHierachy(child);
        }
    }

}
