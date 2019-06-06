using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum orientationMode { NoRot = 0, Line, Target, Node, Node_X, Node_Y, Node_Z }

public class Spline_Controller : MonoBehaviour
{
    public bool autoClose = true;
    public bool autoLoop = true;
    public float duration = 10f;
    public orientationMode rotationMode = orientationMode.Node_Z;
    public GameObject splineRoot;
    public GameObject lookAtTarget;

    public Color gizmoLineColor = Color.red;
    public int gizmoLineSamples = 100;
    public int gizmoRotationSamples = 100;
    public bool gizmoXAxis = false;
    public bool gizmoYAxis = false;
    public bool gizmoZAxis = false;
    public float gizmoAxisLenght = 10f;

    internal class SplineNode
    {
        internal Vector3 Point;
        internal Quaternion Rot;
        internal float Time;
        internal Vector2 EaseIO;

        internal SplineNode(Vector3 p, Quaternion q, float t, Vector2 io) { Point = p; Rot = q; Time = t; EaseIO = io; }
        internal SplineNode(SplineNode o) { Point = o.Point; Rot = o.Rot; Time = o.Time; EaseIO = o.EaseIO; }
    }

    List<SplineNode> splineNodes = new List<SplineNode>();
    Transform[] nodes;

    private float currentTime = 0;
    private int currentIndex = 1;
    private bool followMovement = true;
    private bool SetupDone = false;

    private void Reset()
    {
        splineNodes.Clear();

        SetupDone = false;
        currentIndex = 1;
        currentTime = 0;
    }

    private void Start()
    {
        SetupSplineNodes();
        Destroy(splineRoot);
    }
    
    
    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying)
        {
            SetupSplineNodes();
        }
        else
        {
            for (int i = 0; i < splineNodes.Count; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(splineNodes[i].Point, 1);
            }
        }

        Vector3 prevPos = splineNodes[0].Point;
        
        for (int i = 1; i <= gizmoLineSamples; i++)
        {
            float currTime = i * duration / gizmoLineSamples;
            Vector3 currPos = GetHermiteAtTime(currTime);
            //float mag = (currPos - prevPos).magnitude * 2;
            Gizmos.color = gizmoLineColor;
            Gizmos.DrawLine(prevPos, currPos);
            prevPos = currPos;
        }
        
        for (int j = 1; j <= gizmoRotationSamples; j++)
        {
            float currTime = j * duration / gizmoRotationSamples;
            Vector3 currPos = GetHermiteAtTime(currTime);
            Quaternion rt = GetSquadAtTime(currTime);

            if (gizmoXAxis)
            {
                Vector3 absDirec = rt * Vector3.right;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(currPos, currPos + (absDirec * gizmoAxisLenght));
            }

            if (gizmoYAxis)
            {
                Vector3 absDirec = rt * Vector3.up;
                Gizmos.color = Color.green;
                Gizmos.DrawLine(currPos, currPos + (absDirec * gizmoAxisLenght));
            }

            if (gizmoZAxis)
            {
                Vector3 absDirec = rt * Vector3.forward;
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(currPos, currPos + (absDirec * gizmoAxisLenght));
            }
        }
    }

    private void Update()
    {
        if (SetupDone)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= splineNodes[currentIndex + 1].Time)
            {
                if (currentIndex < splineNodes.Count - 3)
                {
                    currentIndex++;
                }
                else
                {
                    if (!autoLoop)
                    {
                        followMovement = false;

                        transform.position = splineNodes[splineNodes.Count - 2].Point;

                        //if (mRotations) transform.rotation = splineNodes[splineNodes.Count - 2].Rot;
                        //if (mOnEndCallback != null) mOnEndCallback();
                    }
                    else
                    {
                        currentIndex = 1;
                        currentTime = 0;
                    }
                }
            }

            if (followMovement)
            {
                float param = (currentTime - splineNodes[currentIndex].Time) / (splineNodes[currentIndex + 1].Time - splineNodes[currentIndex].Time);
                param = MathUtils.Ease(param, splineNodes[currentIndex].EaseIO.x, splineNodes[currentIndex].EaseIO.y);

                Vector3 HermitePoint = GetHermiteInternal(currentIndex, param);

                if (rotationMode != orientationMode.NoRot)
                {
                    if (rotationMode == orientationMode.Target)
                    {
                        transform.LookAt(lookAtTarget.transform);
                    }
                    else
                    {
                        Quaternion rt = GetSquad(currentIndex, param);
                        if (rotationMode == orientationMode.Node)
                        {
                            transform.rotation = rt;
                        }
                        else
                        {
                            transform.forward = Vector3.RotateTowards(transform.forward, HermitePoint - transform.position, 4 * Time.deltaTime, 0.0f);

                            if (rotationMode != orientationMode.Line)
                            {
                                if (rotationMode == orientationMode.Node_X)
                                {
                                    transform.rotation = Quaternion.Euler(new Vector3(rt.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
                                }
                                else if (rotationMode == orientationMode.Node_Y)
                                {
                                    transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, rt.eulerAngles.y, transform.eulerAngles.z));
                                }
                                else if (rotationMode == orientationMode.Node_Z)
                                {
                                    transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rt.eulerAngles.z));
                                }
                            }
                        }
                    }
                }

                transform.position = HermitePoint;

            }
        }
    }

    private void SetupSplineNodes()
    {
        Reset();

        nodes = InitNodes();

        float step = (autoClose) ? duration / nodes.Length : duration / (nodes.Length - 1);

        for (int i = 0; i < nodes.Length; i++)
        {
            splineNodes.Add(new SplineNode(nodes[i].position, nodes[i].rotation, step * i, new Vector2(0, 1)));
        }
        
        if (autoClose)
        {
            SetAutoCloseMode(step * nodes.Length);
        }
        else
        {
            splineNodes.Insert(0, splineNodes[0]);
            splineNodes.Add(splineNodes[splineNodes.Count - 1]);
        }

        SetupDone = true;
    }

    Transform[] InitNodes()
    {
        if (splineRoot == null)
            return new Transform[] { };

        List<Component> components = new List<Component>(splineRoot.GetComponentsInChildren(typeof(Transform)));
        List<Transform> transforms = components.ConvertAll(c => (Transform)c);

        transforms.Remove(splineRoot.transform);
        transforms.Sort(delegate (Transform a, Transform b)
        {
            return a.name.CompareTo(b.name);
        });

        return transforms.ToArray();
    }

    private void SetAutoCloseMode(float joiningPointTime)
    {
        splineNodes.Add(new SplineNode(splineNodes[0] as SplineNode));
        splineNodes[splineNodes.Count - 1].Time = joiningPointTime;

        Vector3 vInitDir = (splineNodes[1].Point - splineNodes[0].Point).normalized;
        Vector3 vEndDir = (splineNodes[splineNodes.Count - 2].Point - splineNodes[splineNodes.Count - 1].Point).normalized;
        float firstLength = (splineNodes[1].Point - splineNodes[0].Point).magnitude;
        float lastLength = (splineNodes[splineNodes.Count - 2].Point - splineNodes[splineNodes.Count - 1].Point).magnitude;

        SplineNode firstNode = new SplineNode(splineNodes[0] as SplineNode);
        firstNode.Point = splineNodes[0].Point + vEndDir * firstLength;

        SplineNode lastNode = new SplineNode(splineNodes[splineNodes.Count - 1] as SplineNode);
        lastNode.Point = splineNodes[0].Point + vInitDir * lastLength;

        splineNodes.Insert(0, firstNode);
        splineNodes.Add(lastNode);
    }

    private Quaternion GetSquad(int idxFirstPoint, float t)
    {
        Quaternion Q0 = splineNodes[idxFirstPoint - 1].Rot;
        Quaternion Q1 = splineNodes[idxFirstPoint].Rot;
        Quaternion Q2 = splineNodes[idxFirstPoint + 1].Rot;
        Quaternion Q3 = splineNodes[idxFirstPoint + 2].Rot;

        Quaternion T1 = MathUtils.GetSquadIntermediate(Q0, Q1, Q2);
        Quaternion T2 = MathUtils.GetSquadIntermediate(Q1, Q2, Q3);

        return MathUtils.GetQuatSquad(t, Q1, Q2, T1, T2);
    }

    private Vector3 GetHermiteInternal(int idxFirstPoint, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        Vector3 P0 = splineNodes[idxFirstPoint - 1].Point;
        Vector3 P1 = splineNodes[idxFirstPoint].Point;
        Vector3 P2 = splineNodes[idxFirstPoint + 1].Point;
        Vector3 P3 = splineNodes[idxFirstPoint + 2].Point;

        float tension = 0.5f;   // 0.5 equivale a catmull-rom

        Vector3 T1 = tension * (P2 - P0);
        Vector3 T2 = tension * (P3 - P1);

        float Blend1 = 2 * t3 - 3 * t2 + 1;
        float Blend2 = -2 * t3 + 3 * t2;
        float Blend3 = t3 - 2 * t2 + t;
        float Blend4 = t3 - t2;

        return Blend1 * P1 + Blend2 * P2 + Blend3 * T1 + Blend4 * T2;
    }

    private Vector3 GetHermiteAtTime(float timeParam)
    {
        if (timeParam >= splineNodes[splineNodes.Count - 2].Time)
            return splineNodes[splineNodes.Count - 2].Point;

        int i;
        for (i = 1; i < splineNodes.Count - 2; i++)
        {
            if (splineNodes[i].Time > timeParam)
                break;
        }

        int idx = i - 1;
        float param = (timeParam - splineNodes[idx].Time) / (splineNodes[idx + 1].Time - splineNodes[idx].Time);
        param = MathUtils.Ease(param, splineNodes[idx].EaseIO.x, splineNodes[idx].EaseIO.y);

        return GetHermiteInternal(idx, param);
    }

    private Quaternion GetSquadAtTime(float timeParam)
    {
        if (timeParam >= splineNodes[splineNodes.Count - 2].Time)
            return splineNodes[splineNodes.Count - 2].Rot;

        int i;
        for (i = 1; i < splineNodes.Count - 2; i++)
        {
            if (splineNodes[i].Time > timeParam)
                break;
        }

        int idx = i - 1;
        float param = (timeParam - splineNodes[idx].Time) / (splineNodes[idx + 1].Time - splineNodes[idx].Time);
        param = MathUtils.Ease(param, splineNodes[idx].EaseIO.x, splineNodes[idx].EaseIO.y);

        return GetSquad(idx, param);
    }
}


