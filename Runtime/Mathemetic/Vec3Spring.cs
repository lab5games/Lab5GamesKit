using UnityEngine;

public class Vec3Spring 
{
    private Vector3 _origin;
    private Vector3 _curValue;
    private Vector3 _targetValue;
    private Vector3 _change;

    /*
     * 阻尼係數
     * Damping = 1，平穩的移動至目標位置
     * Damping > 1，阻力增加，移動時間變長
     * Damping < 1，阻力減少，快速移動到目標位置，還會過頭來回震盪至漸漸停止
     * Damping = 0，無阻力，不會停止
     */
    private float _damping;
    private float _frequency; //頻率越大，移動到目標位置的速度也越快，但也越容易受振動影響
    private float _sensitivity; //避免微小的計算，當距離小於靈敏度時停止結束

    public Vector3 Target
    {
        get { return _targetValue; }
        set { _targetValue = value; }
    }

    public float Damping
    {
        get { return _damping; }
        set { _damping = value; }
    }

    public float Frequency
    {
        get { return _frequency; }
        set { _frequency = value; }
    }

    public float Sensitivity
    {
        get { return _sensitivity; }
        set { _sensitivity = value; }
    }

    public Vec3Spring(Vector3 origin, float frequency, float damping = 1.0f, float sensitivity = 0.01f)
    {
        _origin = origin;
        _curValue = origin;
        _targetValue = origin;
        _change = Vector3.zero;

        _frequency = frequency;
        _damping = damping;
        _sensitivity = sensitivity;
    }

    public void Reset()
    {
        _curValue = _origin;
        _targetValue = _origin;
        _change = Vector3.zero;
    }

    public Vector3 Update(float deltaTime)
    {
        float ww = _frequency * _frequency;
        float wwt = ww * deltaTime;
        float wwtt = wwt * deltaTime;

        float f = 1.0f + 2.0f * deltaTime * _damping * _frequency;
        float detInv = 1.0f / (f + wwtt);

        _curValue = (_curValue * f + wwtt * _targetValue + deltaTime * _change) * detInv;
        _change = (_change + wwt * (_targetValue - _curValue)) * detInv;

        if (Vector3.Magnitude(_curValue - _targetValue) < _sensitivity)
        {
            _curValue = _targetValue;
        }

        return _curValue;
    }
}
