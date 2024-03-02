using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using PDFtoImage;
using SkiaSharp;
using UnityEngine.UI;

public class PDFConverterHelper : MonoBehaviour
{
    
    public List<Texture2D> images;
    public List<Sprite> sprites;
    
    public Image imageObject;
    
    float raw_aspect_ratio;
    
    void Start()
    {
        images = new List<Texture2D>();
        sprites = new List<Sprite>();
        
        raw_aspect_ratio = imageObject.rectTransform.sizeDelta.x / imageObject.rectTransform.sizeDelta.y;
    }
    
    public void ConvertToImages()
    {
        string pdfPath = "Assets/Resources/Files/University of Ottawa_Kelpie Robotics_Technical Documentation.pdf";
        
        // convert PDF to base64 string
        var pdfBytes = File.ReadAllBytes(pdfPath);
        string base64 = System.Convert.ToBase64String(pdfBytes);
        
        var images_skbmp = PDFtoImage.Conversion.ToImages(
            base64,
            null
            ).ToList();
        
        // convert Skia Bitmaps to Unity Textures
        using (MemoryStream memStream = new MemoryStream())
        using (SKManagedWStream wstream = new SKManagedWStream(memStream))
        {
            foreach (var image in images_skbmp)
            {
                image.Encode(wstream, SKEncodedImageFormat.Png, 100);
                memStream.Position = 0;
                Texture2D tex = new Texture2D(image.Width, image.Height);
                tex.LoadImage(memStream.ToArray());
                images.Add(tex);
                var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                sprites.Add(sprite);
            }
        }
        
        // set the first image to the image object
        SetSprite(0);
        
    }
    
    public void NextImage()
    {
        int currentIndex = sprites.IndexOf(imageObject.sprite);
        if (currentIndex < sprites.Count - 1)
        {
            SetSprite(currentIndex + 1);
        }
    }
    
    public void PreviousImage()
    {
        int currentIndex = sprites.IndexOf(imageObject.sprite);
        if (currentIndex > 0)
        {
            SetSprite(currentIndex - 1);
        }
    }
    
    private void SetSprite(int index)
    {
        imageObject.sprite = sprites[index];
        // match the image object's size to the sprite's size, maintaining the aspect ratio of the sprite and with 
        // upper image size bounds of 15000 width and 10000 height
        float width = imageObject.sprite.texture.width;
        float height = imageObject.sprite.texture.height;
        float aspectRatio = (float)imageObject.sprite.texture.width / imageObject.sprite.texture.height;
        if (aspectRatio > raw_aspect_ratio)
        {
            width = 15000f;
            height = width / aspectRatio;
        }
        else
        {
            height = 10000f;
            width = height * aspectRatio;
        }
        imageObject.rectTransform.sizeDelta = new Vector2(width, height);
    }
    
}
