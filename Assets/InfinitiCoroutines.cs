using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfinitiCoroutines : MonoBehaviour
{
    [SerializeField] private Button _startStopButton;
    [SerializeField] private Image _coroutine1Indicator;
    [SerializeField] private Image _coroutine2Indicator;
    [SerializeField] private TMP_Text _textField1;
    [SerializeField] private TMP_Text _textField2;

    private Coroutine currentCoroutine;
    private bool isRunning = false;
    private bool _isNewLoop = true;
    private int _firstLoopIndex = -1;

    private void Start()
    {
        _startStopButton.onClick.AddListener(StartStopButtonClicked);
        SetIndicatorsColor(Color.yellow);
        _textField1.text = "0";
        _textField2.text = "0";
    }

    private void StartStopButtonClicked()
    {
        if (isRunning)
        {
            StopAllCoroutines();
            SetIndicatorsColor(Color.yellow);
            _textField1.text = "0";
            _textField2.text = "0";
            _startStopButton.GetComponentInChildren<TMP_Text>().text = "Start";
            isRunning = false;
            _isNewLoop = true;
        }
        else
        {
            SetIndicatorsColor(Color.red);
            currentCoroutine = StartCoroutine(RunCoroutines());           
            _startStopButton.GetComponentInChildren<TMP_Text>().text = "Stop";
            isRunning = true;
            _isNewLoop = false;
        }
    }

    IEnumerator RunCoroutines()
    {
        while (true)
        {
            if (_isNewLoop)
            {
                int randomCoroutine = Random.Range(1, 3);
                _firstLoopIndex = randomCoroutine;
                if (randomCoroutine == 1)
                {
                    yield return StartCoroutine(ExecuteCoroutine(1, _coroutine1Indicator, _textField1));
                }
                else
                {
                    yield return StartCoroutine(ExecuteCoroutine(2, _coroutine2Indicator, _textField2));
                }
            }
            else
            {
                if (_firstLoopIndex == 1)
                {
                    yield return StartCoroutine(ExecuteCoroutine(2, _coroutine2Indicator, _textField2));
                    yield return StartCoroutine(ExecuteCoroutine(1, _coroutine1Indicator, _textField1));                    
                }
                else
                {
                    yield return StartCoroutine(ExecuteCoroutine(1, _coroutine1Indicator, _textField1));
                    yield return StartCoroutine(ExecuteCoroutine(2, _coroutine2Indicator, _textField2));
                }                
            }
        }
    }

    private IEnumerator ExecuteCoroutine(int id, Image indicator, TMP_Text textField)
    {
        SetIndicatorColor(indicator, Color.green);
        int timerValue = Random.Range(10, 21);

        for (int i = timerValue; i >= 0; i--)
        {
            textField.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        SetIndicatorColor(indicator, Color.red);
    }

    private void SetIndicatorsColor(Color color)
    {
        SetIndicatorColor(_coroutine1Indicator, color);
        SetIndicatorColor(_coroutine2Indicator, color);
    }

    private void SetIndicatorColor(Image indicator, Color color)
    {
        indicator.color = color;
    }
}
