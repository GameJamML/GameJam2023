using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _currentState = 0;
    [SerializeField] private float[] _statePos;

    public int currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            if (value >= 0 && value <= 4)
                _currentState = value;
        }
    }

    void Update()
    {
        StateBehavior(_currentState);
    }

    public void StateBehavior(int state)
    {
        _currentState = state;

        switch (state)
        {
            case 0:
                //No se ha cogido ningún objetivo.
                //El perseguidor no aparece en el radar.
                //No hay ningún sonido de tensión
                break;
            case 1:
                //Empieza tras coger el primer objetivo, al cogerlo se emite un sonido de tensión(un rugido, algo estridente, )
                //La primera vez que le das al radar la criatura aparece como un punto rojo en la lejanía, las siguientes pueden ser punto rojo, verde o ninguno.
                //Hay un sonido continuo de tensión
                break;
            case 2:
                //Empieza tras coger el segundo objetivo.
                //El perseguidor aparece en el radar como un punto rojo o no aparece.
                //Ya no esta en el borde del radar, cuando aparece, aparece dentro del área.
                transform.localPosition = new Vector3(0, 0, _statePos[0]);
                break;
            case 3:
                //Empieza tras coger el tercer objetivo.
                //El perseguidor siempre aparece en el radar como un punto rojo más cerca que antes.
                transform.localPosition = new Vector3(0, 0, _statePos[1]);
                break;
            case 4:
                //Al llegar al último objetivo y utilizar el foco este no funciona. El objetivo se empieza a mover y es cuando la batería falla y ocurre el evento final.
                break;
        }
    }
}
