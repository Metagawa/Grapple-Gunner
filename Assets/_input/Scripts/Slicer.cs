using System;
using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public Material slicedEdgeMaterial;
    public LayerMask sliceMask;
    public bool isTouched;
    public DissolveChilds SlicedParentHandler;
    private MeshRenderer m_meshRenderer;
    private MeshRenderer n_meshRenderer;
    int slicedCount;

    private void Start()
    {
    }

    private void Update()
    {

        if (isTouched == true)
        {
            isTouched = false;
            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, slicedEdgeMaterial);
                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, slicedEdgeMaterial);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, slicedEdgeMaterial);
                //upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                //lowerHullGameobject.transform.position = objectToBeSliced.transform.position;
                upperHullGameobject.transform.SetParent(objectToBeSliced.gameObject.transform.parent,false);
                lowerHullGameobject.transform.SetParent(objectToBeSliced.gameObject.transform.parent,false);
                Destroy(objectToBeSliced.gameObject);
                m_meshRenderer = upperHullGameobject.GetComponent<MeshRenderer>();
                n_meshRenderer = lowerHullGameobject.GetComponent<MeshRenderer>();
                //m_meshRenderer.material = materialAfterSlice;
                //n_meshRenderer.material = materialAfterSlice;
                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);
                Destroy(upperHullGameobject.gameObject, 5);
                Destroy(lowerHullGameobject.gameObject, 5);

            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.layer = 7;
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
