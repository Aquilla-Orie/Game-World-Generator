using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 enum RoomItems
{
    BACKGROUND,
    WALL,
    BENCH,
    TABLE,
    COLLECTABLE,
    BOOKSHELF,
    TORCH,
    DOOR,
    CRATE
}

public class ImageProcessor : MonoBehaviour
{
    [SerializeField] private Texture2D _generatedDungeon;
    [SerializeField] private List<Texture2D> _generatedRooms;
    [SerializeField] private GameObject _backgroundObject;
    [SerializeField] private GameObject _roomObject;
    private int _width;
    private int _height;

    private List<Vector2> _roomCoords;


    private void Start()
    {
        //ProcessDungeon();
        ProcessRooms();
    }
    private void ProcessDungeon()
    {
        _roomCoords = new List<Vector2>();

        _width = _generatedDungeon.width;
        _height = _generatedDungeon.height;

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {

                Color tPix = _generatedDungeon.GetPixel(i, j);

                if(tPix == Color.white)_roomCoords.Add(new Vector2(i, j));

                Instantiate(tPix == Color.white ? _roomObject : _backgroundObject, new Vector3(i*_roomObject.transform.localScale.x, 0, j* _roomObject.transform.localScale.z), Quaternion.identity);

            }
        }

    }

    private void ProcessRooms()
    {

        foreach (var room in _generatedRooms)
        {
            _width = room.width;
            _height = room.height;

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Color tPix = room.GetPixel(i, j);
                    var obj = Instantiate(_roomObject, new Vector3(i, 0, j), Quaternion.identity);

                    int objType = (int)((tPix.r * 8) + (tPix.g * 8) + (tPix.b * 8))/3;


                    obj.GetComponent<MeshRenderer>().material.color = tPix;
                    obj.transform.localScale = Vector3.one;
                    obj.name = $"({i},{j}), {objType},{(RoomItems)objType}";
                    Debug.Log($"{i},{j} :: {tPix}");
                }
            }
        }
    }

}
