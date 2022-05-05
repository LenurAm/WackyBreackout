using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    /// <summary>
    /// Level builder
    /// </summary>
    [SerializeField]
    GameObject prefabPaddle;
    [SerializeField]
    GameObject prefabStandardBlock;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefabPaddle);
        
        //retrieve the standart block size
        GameObject tempBlock=Instantiate<GameObject>(prefabStandardBlock);
        BoxCollider2D boxCollider = tempBlock.GetComponent<BoxCollider2D>();
        float blockWidth = boxCollider.size.x;
        float blockHeight = boxCollider.size.y;
        Destroy(tempBlock);

        //get the screen size
        float screenWidth=ScreenUtils.ScreenRight-ScreenUtils.ScreenLeft;
        float screenHeight=ScreenUtils.ScreenTop-ScreenUtils.ScreenBottom;
        

        //calculate the number of blocks per row
        int blockNumbers=(int)(screenWidth/blockWidth);
      
        float totalBlockWidth = blockNumbers * blockWidth;

        //the centre of the block's row
        float blocksRowCentre = ScreenUtils.ScreenLeft + (screenWidth - totalBlockWidth) / 2 + blockWidth / 2;
        //put the row by height
        float heightOfRow = ScreenUtils.ScreenTop - screenHeight / 5 - blockHeight / 2;
       
        //build 3 rows of blocks
        Vector2 curPosition=new Vector2(blocksRowCentre, heightOfRow);
        for (int row=0; row<3; row++)
        {
            
            for (int column=0; column<blockNumbers;column++)
            {
                Instantiate(prefabStandardBlock, curPosition, Quaternion.identity);
                curPosition.x += blockWidth;
                
            }
            curPosition.x = blocksRowCentre;
            curPosition.y=curPosition.y+2*blockHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
