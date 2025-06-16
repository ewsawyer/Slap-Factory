/*
 * I didn't do the math! All the math in this script is from a video by t3ssel8r on YouTube
 * https://www.youtube.com/watch?v=KPoeNZZ6H4s&t=2s
 */

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IDynamicsListener
{
    public void OnDynamicsUpdate(SecondOrderDynamics sod);
}

public class SecondOrderDynamics : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Frequency. Affects speed at which the system responds, and the frequency of vibration")]
    [SerializeField] private float F;
    [Tooltip("Damping coefficient")]
    [SerializeField] private float Z;
    [Tooltip("Response. Negative values cause anticipation. Values on (0, 1] cause immediate response. Values > 1 cause overshoot")]
    [SerializeField] private float R;
    
    public Vector3 X;  // the input variable. other scripts can change this to affect the system
    
    private Vector3 xp;  // previous input
    public Vector3 y, yd;  // state variables. the position and velocity of the system
    private float _w, _z, _d, k1, k2, k3;  // constants

    // A list of objects that are listening to the system, and will be notified when it updates
    private List<IDynamicsListener> _listeners;

    private void Awake()
    {
        Init(F, Z, R, X);
    }

    private void Update()
    {
        UpdateDynamics(Time.deltaTime, X);
    }

    public void Init(float f, float z, float r, Vector3 x0)
    {
        // compute constants
        _w = 2 * Mathf.PI * f;
        _z = z;
        _d = _w * Mathf.Sqrt(Mathf.Abs(z * z - 1));
        k1 = z / (Mathf.PI * f);
        k2 = 1.0f / (_w * _w);
        k3 = r * z / _w;
        // initialize variables
        xp = x0;
        y = x0;
        yd = Vector3.zero;
        // initialize list of listeners
        if (_listeners == null)
            _listeners = new List<IDynamicsListener>();
    }

    public void AddListener(IDynamicsListener l)
    {
        if (_listeners == null)
            _listeners = new List<IDynamicsListener>();
        
        _listeners.Add(l);
    }

    public void RemoveListener(IDynamicsListener l)
    {
        _listeners.Remove(l);
    }
    
    public virtual Vector3 UpdateDynamics(float T, Vector3 x, Vector3 xd)
    {
        float k1_stable, k2_stable;
        if (_w * T < _z)  // clamp k2 to guarantee stability without jitter
        { 
            k1_stable = k1;
            k2_stable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);
        }
        else  // use pole matching when the system is very fast
        {
            float t1 = Mathf.Exp(-_z * _w * T);
            float alpha = 2 * t1 * (_z <= 1 ? Mathf.Cos(T * _d) : Unity.Mathematics.math.cosh(T * _d));
            float beta = t1 * t1;
            float t2 = T / (1 + beta - alpha);
            k1_stable = (1 - beta) * t2;
            k2_stable = T * t2;
        }

        y = y + T * yd;  // integrate position by velocity
        yd = yd + T * (x + k3 * xd - y - k1_stable * yd) / k2_stable;  // integrate velocity by acceleration

        NotifyListeners();
        return y;
    }

    private void NotifyListeners()
    {
        foreach(IDynamicsListener listener in _listeners)
            listener.OnDynamicsUpdate(this);
    }

    // Overloaded in case we don't know the velocity of the input
    public Vector3 UpdateDynamics(float T, Vector3 x)
    {
        // estimate velocity of the input, xd
        Vector3 xd = (x - xp) / T;
        xp = x;
        return UpdateDynamics(T, x, xd);
    }
}
