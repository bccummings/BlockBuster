using System.Linq;
using UnityEngine;

public class AttachBlocks : MonoBehaviour
{
    [SerializeField] float breakForce;
    [SerializeField] float distanceThreshold;

    // Start is called before the first frame update
    void Start()
    {
        CreateFixedJoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateFixedJoints()
    {
        foreach (Transform iChild in transform)
        {
            foreach (Transform jChild in transform)
            {

                // Calculate distance at nearest point
                // float distance = Vector3.Distance(
                //     iChild.GetComponent<Collider>().ClosestPoint(jChild.position),
                //     jChild.GetComponent<Collider>().ClosestPoint(iChild.position)
                // );

                float distance = Vector3.Distance(iChild.localPosition, jChild.localPosition);

                if (
                        (iChild != jChild) & // don't connect to self
                        (iChild != this.transform) & (jChild != this.transform) & // don't connect to parent object
                        (iChild.tag == "Block") & (jChild.tag == "Block") & // only connect to parts labeled "block"
                        (distance <= distanceThreshold) // only connect if the parts are in close proximity
                        //TODO: check if there is a fixed joint going the opposite direction
                        //TODO: Only want fixed joints in vertical directions, not horizontal
                    )
                {
                    FixedJoint joint = iChild.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = jChild.GetComponent<Rigidbody>();
                    joint.breakForce = breakForce;
                }
            }
        }
    }
}
