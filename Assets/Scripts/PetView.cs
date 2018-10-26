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
            PetViewCollider = GetComponent<Collider2D>();
            PetSprite = GetComponent<SpriteRenderer>();

            if (PetData.PetSpriteName != null)
            {
                var sprite = Resources.LoadAll(PetData.PetSpriteName, typeof(Sprite));
                if (sprite.Length > 0)
                {
                    PetSprite.sprite= (Sprite)sprite[0];
                }
            }
        }



        void Update()
        {
            if(CheckTouched())
            {
                MouseDownEvents.Invoke();

                /* Moveing the pet view */
                PetViewController.instance.FocusPetView(this);
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
