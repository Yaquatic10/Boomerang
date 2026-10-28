using UnityEngine;

public class CtxDataBase : MonoBehaviour
{
    private int _p1CharIndex;
    private int _p2CharIndex;
    string _p1ControlSchemeName;
    string _p2ControlSchemeName;
    
    //SETTERS Y GETTERS.
    public int P1CharIndex {  get {  return _p1CharIndex; } set { _p1CharIndex = value; } }
    public int P2CharIndex {  get { return _p2CharIndex; } set { _p2CharIndex = value; } }
    public string P1ControlSchemeName {  get { return _p1ControlSchemeName; } set { _p1ControlSchemeName = value; } }
    public string P2ControlSchemeName { get { return _p2ControlSchemeName; } set { _p2ControlSchemeName = value; } }


}
