using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpiritPetMaster
{
    public class PetView : Pet
    {
        public UnityEvent MouseDownEvents;
        public int MoodIncreatedRate = 1;
        public LayerMask TouchLayer;
        public float TimeForHunger = 3;


        #region private

        SpriteRenderer PetSprite;
        Animator PetAnimator;
        Collider2D PetViewCollider;

        float HungerTimer;

        #endregion



        void Start()
        {
            LoadData();

            PetViewCollider = GetComponent<Collider2D>();
            PetSprite = GetComponent<SpriteRenderer>();
            PetAnimator = GetComponent<Animator>();

            /* Loading the image of the pet */
            if (this.PetSpriteName != "")
            {
                var sprite = Resources.Load(this.PetSpriteName, typeof(Sprite));
                if (sprite != null)
                {
                    PetSprite.sprite= (Sprite)sprite;
                }
            }

            /* Loading the animator of the pet */
            if(this.PetAnimatorName != "")
            {
                var controller = (RuntimeAnimatorController)Resources.Load(this.PetAnimatorName, typeof(RuntimeAnimatorController));
                if(controller != null)
                {
                    PetAnimator.runtimeAnimatorController = controller;
                }
                else
                {
                    Debug.LogFormat("pet animator not found: {0}", this.PetAnimatorName);
                }
            }
            else
            {
                Debug.LogFormat("pet animator not set: {0}", this.PetAnimatorName);
            }

            /* Loading the taking of th pet */
            if(this.PetTalkingFilename != "")
            {
                TextAsset text;

                //normal talks
                text = Resources.Load(this.PetTalkingFilename + "_normal") as TextAsset;
                if(text != null)
                {
                    //Debug.LogFormat("{0}: {1}", this.PetTalkingFilename, text.text);
                    PetTalkingContents content = JsonUtility.FromJson<PetTalkingContents>(text.text);
                    if (content != null)
                    {
                        this.PetTalkingContents_normal = content.contents;
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

                // hungry talk
                text = Resources.Load(this.PetTalkingFilename + "_hungry") as TextAsset;
                if(text != null)
                {
                    //Debug.LogFormat("{0}: {1}", this.PetTalkingFilename, text.text);
                    PetTalkingContents content = JsonUtility.FromJson<PetTalkingContents>(text.text);
                    if (content != null)
                    {
                        this.PetTalkingContents_hungry = content.contents;
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

                //happy talk
                text = Resources.Load(this.PetTalkingFilename + "_happy") as TextAsset;
                if(text != null)
                {
                    //Debug.LogFormat("{0}: {1}", this.PetTalkingFilename, text.text);
                    PetTalkingContents content = JsonUtility.FromJson<PetTalkingContents>(text.text);
                    if (content != null)
                    {
                        this.PetTalkingContents_happy = content.contents;
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

            HungerTimer = Time.time;
        }



        void Update()
        {
            /* Sorting the layer */
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 1000f) ;
            // if(CheckTouched())
            // {
            //     MouseDownEvents.Invoke();

            //     /* Moveing the pet view */
            //     PetViewController.instance.TouchPetView(this);
            // }
            ChangeTalksType();

            if(Time.time-HungerTimer>TimeForHunger)
            {
                DecreaseHunger((int)((Time.time-HungerTimer)/TimeForHunger));

                HungerTimer = Time.time;
            }

        }



        void OnDisable()
        {
            SaveData();
        }



        public void IncreateMood()
        {
            if(Hunger>0)
            {
                this.Mood += MoodIncreatedRate;
                if(this.Mood > 100)
                {
                    this.Mood = 100;
                }
                PetAnimator.SetFloat("Mood", this.Mood);
            }
            // PetInformation.instance.UpdateInfo();
            PetInformation.instance.ChangeTakingContent();
        }



        public void IncreateHunger(int _value)
        {
            this.Hunger += _value;
            if(this.Hunger > 100)
            {
                this.Hunger = 100;
            }
            PetAnimator.SetTrigger("isEating");
            PetInformation.instance.UpdateInfo();
        }

        public void DecreaseHunger(int _value)
        {
            this.Hunger -= _value;
            if(this.Hunger < 0)
            {
                this.Hunger = 0;
                this.Mood -= 1;
                if(this.Mood<0)
                    this.Mood = 0;
                PetAnimator.SetFloat("Mood", this.Mood);
            }
        }

        public void ChangeTalksType()
        {
            if(Hunger < 5)
                PetTalkingContents = PetTalkingContents_hungry;
            else if(Mood>HappyMood)
                PetTalkingContents = PetTalkingContents_happy;
            else
                PetTalkingContents = PetTalkingContents_normal;
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
        void OnMouseDown()
        {
            if(!EventSystem.current.IsPointerOverGameObject()){

                // check that it worked
                //Debug.Log("Player has clicked " + gameObject.name);

                /* Moveing the pet view */
                PetViewController.instance.TouchPetView(this);
            }
        }
        
    }
}
