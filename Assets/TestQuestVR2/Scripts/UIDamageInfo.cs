using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDamageInfo : MonoBehaviour
{
    [SerializeField] private DamageGetter _target ;

    [SerializeField] private TextMeshProUGUI _prefabTMPInfo;

    [Range(1f, 10f)]
    [SerializeField] private float _maxLifeTime = 2f;

    private List<TextMeshProUGUI> _infoEnableTMPs = new();

    private List<TextMeshProUGUI> _infoDisableTMPs = new();

    private void OnEnable()
    {
        _target.OnGetDamage += GetDamage;
    }

    private void OnDisable()
    {
        _target.OnGetDamage -= GetDamage;
    }

    private void GetDamage(int damage)
    {
        if (_infoDisableTMPs.Count == 0)
        {
            EnableTMP(Instantiate(_prefabTMPInfo, transform), damage);
        }
        else
        {
            TextMeshProUGUI tmpInfo = _infoDisableTMPs[0];

            _infoDisableTMPs.RemoveAt(0);

            EnableTMP(tmpInfo, damage);
        }
    }

    private void EnableTMP(TextMeshProUGUI tmpInfo, int damage)
    {
        tmpInfo.gameObject.SetActive(true);

        tmpInfo.transform.localPosition = Vector3.zero;

        tmpInfo.color = Color.white;

        tmpInfo.text = damage.ToString();

        _infoEnableTMPs.Add(tmpInfo);

        StartCoroutine(WaitToDisable(tmpInfo));
    }

    private void DisableTMP(TextMeshProUGUI tmpInfo)
    {
        tmpInfo.gameObject.SetActive(false);

        _infoEnableTMPs.Remove(tmpInfo);

        _infoDisableTMPs.Add(tmpInfo);
    }

    private IEnumerator WaitToDisable(TextMeshProUGUI tmpInfo)
    {
        float x = Random.Range(-100f, 100f);

        float y = Random.Range(-100f, 100f);

        float time = Random.Range(1, _maxLifeTime);

        float timer = 0;

        Vector3 direction = new (x, y, 0);

        float r = Random.Range(0f, 1f);

        float g = Random.Range(0f, 1f);

        float b = Random.Range(0f, 1f);

        tmpInfo.color = new Color(r, g, b, 1);

        while (timer < time)
        {
            tmpInfo.transform.localPosition += direction * Time.deltaTime;

            tmpInfo.color = new Color(r, g, b, 1f - ( timer / (time / 100f) / 100) );

            timer += Time.deltaTime;

            yield return null;
        }

        DisableTMP(tmpInfo);
    }
}
