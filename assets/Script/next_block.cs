using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class next_block : MonoBehaviour
{
    public Sprite newBlock1;
    public Sprite newBlock2;
    public Sprite newBlock3;
    public Sprite newBlock4;
    // public Sprite newSprite;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
    // SpriteRendererコンポーネントを取得
    spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    // 画像を切り替えます
        int temp_block = GameManager.next_block; 
        //Debug.Log(temp_block);
        switch(temp_block)
                {
                    case 1:
                        spriteRenderer.sprite = newBlock1;
                        break;
                    case 2:
                        spriteRenderer.sprite = newBlock2;
                        break;
                    case 3:
                        spriteRenderer.sprite = newBlock3;
                        break;
                    case 4:
                        spriteRenderer.sprite = newBlock4;
                        break;
                    //case 5:
                    //    newSprite = block_5_Prefab;
                    //    break;
                    //image.sprite = newSprite;
                }
    }
}
