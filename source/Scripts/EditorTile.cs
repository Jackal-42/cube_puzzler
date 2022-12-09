using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTile : MonoBehaviour
{
    EditorManager manager;
    public SpriteRenderer sprite;
    public int value = 0;
    public int variant = 0;
    public int rotation = 0;
    void Start()
    {
        value = 0;
        sprite = GetComponent<SpriteRenderer>();
        manager = (EditorManager)FindObjectOfType(typeof(EditorManager));
        sprite.sprite = manager.drawableTiles[value].editorTexture;
        transform.localRotation = Quaternion.Euler(0, 0, manager.rotation);
    }

    void OnMouseOver()
    {
        var dt = manager.drawableTiles[manager.tileIndex];
        sprite.sprite = dt.editorTexture;
        transform.localRotation = Quaternion.Euler(0, 0, manager.rotation);
        if (dt.tintable)
        {
            sprite.color = manager.vc[manager.variant];
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    void OnMouseEnter()
    {
        var dt = manager.drawableTiles[manager.tileIndex];
        sprite.sprite = dt.editorTexture;

        if (dt.tintable)
        {
            sprite.color = manager.vc[manager.variant];
        }
        else
        {
            sprite.color = Color.white;
        }
        
        transform.localRotation = Quaternion.Euler(0, 0, manager.rotation);
        if (Input.GetMouseButton(0))
        {
            sprite.sprite = dt.editorTexture;
            
            value = manager.tileIndex;
            if (dt.rotatable)
            {
                rotation = manager.rotation;
            }
            if (dt.variantable)
            {
                variant = manager.variant;
                if (dt.tintable)
                {
                    sprite.color = manager.vc[manager.variant];
                }
            }
            else
            {
                sprite.color = Color.white;
            }

            transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    void OnMouseExit()
    {
        var dt = manager.drawableTiles[value];
        sprite.sprite = dt.editorTexture;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
        if (dt.tintable)
        {
            sprite.color = manager.vc[variant];
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.parent.parent.parent.GetComponent<EditorSection>().Rotate();
        }
        else
        {
            var dt = manager.drawableTiles[manager.tileIndex];
            sprite.sprite = dt.editorTexture;
            value = manager.tileIndex;
            if (dt.rotatable)
            {
                rotation = manager.rotation;
            }
            if (dt.variantable)
            {
                variant = manager.variant;
                if (dt.tintable)
                {
                    sprite.color = manager.vc[manager.variant];
                }
            }
            else
            {
                sprite.color = Color.white;
            }
        }
    }
}
