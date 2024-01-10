using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteImagesController : MonoBehaviour
{
    [SerializeField]
    private SpriteImages _spriteImages;
    private Dictionary<string, Sprite> _spriteImagesDictionary = new Dictionary<string, Sprite>();
    // Start is called before the first frame update

    public void InitializeImages()
    {
        foreach (SpriteImage spriteImage in _spriteImages.SpriteImagesList)
        {
            _spriteImagesDictionary.Add(spriteImage.imageName, spriteImage.image);
        }
    }

    public void SetSpriteImage(string imageName, Image sprite)
    {
        imageName = imageName.ToLower();
        if(_spriteImagesDictionary.ContainsKey(imageName)){
            sprite.sprite = _spriteImagesDictionary[imageName];
        }else{
            Debug.LogWarning("Image " + imageName + " not found in SpriteImages.");
        }
        
    }


}
