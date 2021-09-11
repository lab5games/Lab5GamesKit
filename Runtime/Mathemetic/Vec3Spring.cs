using UnityEngine;

public class Vec3Spring 
{
    private Vector3 _origin;
    private Vector3 _curValue;
    private Vector3 _targetValue;
    private Vector3 _change;

    /*
     * �����Y��
     * Damping = 1�A��í�����ʦܥؼЦ�m
     * Damping > 1�A���O�W�[�A���ʮɶ��ܪ�
     * Damping < 1�A���O��֡A�ֳt���ʨ�ؼЦ�m�A�ٷ|�L�Y�Ӧ^�_���ܺ�������
     * Damping = 0�A�L���O�A���|����
     */
    private float _damping;
    private float _frequency; //�W�v�V�j�A���ʨ�ؼЦ�m���t�פ]�V�֡A���]�V�e�������ʼv�T
    private float _sensitivity; //�קK�L�p���p��A��Z���p���F�ӫ׮ɰ����

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
