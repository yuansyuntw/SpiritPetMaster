using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

namespace SpiritPetMaster
{
    public class PetView : Pet
    {
        public UnityEvent MouseDownEvents;
        public int MoodIncreatedRate = 1;
        public LayerMask TouchLayer;

        #region private

        SpriteRenderer PetSprite;
        Collider2D PetViewCollider;

        #endregion



        void Start()
        {
            LoadData();

            PetViewCollider = GetComponent<Collider2D>();
            PetSprite = GetComponent<SpriteRenderer>();

            /* Loading the image of the pet */
            if (this.PetSpriteName != "")
            {
                var sprite = Resources.Load(this.PetSpriteName, typeof(Sprite));
                if (sprite != null)
                {
                    PetSprite.sprite= (Sprite)sprite;
                }
            }

            /* Loading the taking of th pet */
            if(this.PetTalkingFilename != "")
            {
                TextAsset text = Resources.Load(this.PetTalkingFilename) as TextAsset;
                if(text != null)
                {
                    //Debug.LogFormat("{0}: {1}", this.PetTalkingFilename, text.text);
                    PetTakingContents content = JsonUtility.FromJson<PetTakingContents>(text.text);
                    if (content != null)
                    {
                        this.PetTakingContents = content.contents;
                    }
                    else
                    {
                        Debug.LogFormat("can't parse the json file");
                    }
                    
                }
                else
                {
                    Debug.LogFormat("not found taking file: {0}", this.PetTalkingFilename);
                }
            }
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



        void OnDisable()
        {
            SaveData();
        }



        public void IncreateMood()
        {
            this.Mood += MoodIncreatedRate;
            PetInformation.instance.UpdateInfo();
            PetInformation.instance.ChangeTakingContent();
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
