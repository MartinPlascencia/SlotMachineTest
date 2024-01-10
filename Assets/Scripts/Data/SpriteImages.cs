using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteImage
{
    public string imageName;
    public Sprite image;
}
[CreateAssetMenu(fileName = "SpriteImages", menuName = "ScriptableObjects/CreateSpriteImagesData", order = 1)]
public class SpriteImages : ScriptableObject
{
    [SerializeField]
    private List<SpriteImage> _spriteImages;
    public List<SpriteImage> SpriteImagesList { get { return _spriteImages; } }
}
