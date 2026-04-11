using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    [SerializeField] private int _maxPushes = 3;
    [SerializeField] private TMP_Text m_TextComponent; // assign in inspector OR auto-find

    private int _pushCount = 0;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // 🔧 Auto-find text if not assigned
        if (m_TextComponent == null)
            m_TextComponent = GetComponentInChildren<TMP_Text>();

        UpdateText();
    }

    public bool CanBePushed()
    {
        return _pushCount < _maxPushes;
    }

    public void RegisterPush()
    {
        _pushCount++;
        UpdateText();
    }

    public void SetMaxPushes(int value)
    {
        _maxPushes = value;
        _pushCount = 0;
        UpdateText();
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    void UpdateText()
    {
        if (m_TextComponent == null) return;

        int remaining = _maxPushes - _pushCount;
        m_TextComponent.text = remaining.ToString();
        m_TextComponent.color = Color.black;
    }
}