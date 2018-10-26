using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

namespace SpiritPetMaster
{
    public class PetView : MonoBehaviour
    {
        public UnityEvent MouseDownEvents;
        public int MoodIncreatedRate = 1;
        public LayerMask TouchLayer;

        [Header("Pet Data")]
        public Pet PetData;

        #region private
        SpriteRenderer PetSprite;
        Collider2D PetViewCollider;
        #endregion


        void Start()
        {
<<<<<<< HEAD
            PetViewCollider = GetComponent<Collider2D>();
            PetSprite = GetComponent<SpriteRenderer>();

            /* Loading the image of the pet */
            if (PetData.PetSpriteName != "")
            {
                var sprite = Resources.Load(PetData.PetSpriteName, typeof(Sprite));
                if (sprite != null)
                {
                    PetSprite.sprite= (Sprite)sprite;
                }
            }

            /* Loading the taking of th pet */
            if(PetData.PetTalkingFilename != "")
            {
                TextAsset text = Resources.Load(PetData.PetTalkingFilename) as TextAsset;
                if(text != null)
                {
                    Debug.LogFormat("{0}: {1}", PetData.PetTalkingFilename, text.text);
                    /*
                    PetTakingContents content = JsonUtility.FromJson<PetTakingContents>(text.text);
                    if (content != null)
                    {
                        PetData.PetTakingContents = content.contents;
                    }
                    else
                    {
                        Debug.LogFormat("can't parse the json file");
                    }
                    */
                }
                else
                {
                    Debug.LogFormat("not found taking file: {0}", PetData.PetTalkingFilename);
                }
            }
=======
            // if(PetData.PetSprite != null)
            // {
            //     PetImage.sprite = PetData.PetSprite;
            // }
>>>>>>> animation
        }



        void Update()
        {
            if(CheckTouched())
            {
                MouseDownEvents.Invoke();

                /* Moveing the pet view */
                PetViewController.instance.TouchPetView(this);
            }
        }



        public void IncreateMood()
        {
            PetData.Mood += MoodIncreatedRate;
            PetInformation.instance.UpdateInfo();
        }



        bool CheckTouched()
        {
            bool result = false;

            /* lifting up */
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 w_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mouse_pos = new Vector2(w_point.x, w_point.y);
                if (PetViewCollider == Physics2D.OverlapPoint(mouse_pos, TouchLayer))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
