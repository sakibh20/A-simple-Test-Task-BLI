using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace _Project.Scripts
{
    public enum Actions
    {
        Translate,
        Rotate
    }
    public class Instantiatable : MonoBehaviour, ISelectable
    {
        private Vector3 _lastPos;
        private float _normalY;
        private float _upY;
        private readonly float _tweenTime = 0.8f;
        private readonly float _tweenValue = 1.0f;
        private Sequence _selectSequence;
        private bool _selected;

        private Transform _transform;
        private SelectionManager _selectionManager;
        private ReferenceManager _referenceManager;
        private DragableObject _dragableObject;
        private Actions _actionOnSelect = Actions.Translate;

        private void Awake()
        {
            _dragableObject = GetComponent<DragableObject>();
        }

        private void Start()
        {
            _referenceManager = ReferenceManager.instance;
            
            _transform = transform;
            
            _lastPos = _transform.localPosition;

            _normalY = _lastPos.y;
            _upY = _normalY + (_tweenValue * _transform.localScale.y);
        }

        public void OnSelect()
        {
            if (_selected)
            {
                return;
            }

            _dragableObject.canDrag = true;
            _selected = true;
            _dragableObject.translate = true;
            CustomDebug.LogWarning("Manage On Select");
            TweenYPos();
            _referenceManager.selectionManager.OnSelected(this);
        }

        public void OnDeSelect()
        {
            _dragableObject.canDrag = false;
            _selected = false;
            _selectSequence.Kill();
            _actionOnSelect = Actions.Translate;
            //_transform.DOKill();
            _transform.localPosition = new Vector3(_transform.localPosition.x, _normalY, _transform.localPosition.z);
        }

        private void TweenYPos()
        {
            _selectSequence = DOTween.Sequence();

            _selectSequence.Append(_transform.DOLocalMoveY(_upY, _tweenTime).SetEase(Ease.OutExpo));
            _selectSequence.Append(_transform.DOLocalMoveY(_normalY, _tweenTime).SetEase(Ease.InExpo));
            _selectSequence.SetLoops(-1);

            _selectSequence.Play();
        }

        public void PauseTween()
        {
            _selectSequence.Pause();
        }
        
        public void ResumeTween()
        {
            _selectSequence.Restart();
        }


        private void Update()
        {
            if (!_selected)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                _referenceManager.selectionManager.OnDeleted(this);

                _referenceManager.modeManager.ChangeMode(Modes.Instantiate);

                //_transform.DOKill();
                _selectSequence.Kill();
                
                Destroy(gameObject);
            }
            
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (_actionOnSelect == Actions.Translate)
                {
                    _actionOnSelect = Actions.Rotate;
                    _dragableObject.translate = false;
                }
                else
                {
                    _actionOnSelect = Actions.Translate;
                    _dragableObject.translate = true;
                }
            }
        }
    }
}
