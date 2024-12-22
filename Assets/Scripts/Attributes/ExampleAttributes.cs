using UnityEngine;

public class Example : BaseMonoBehaviour
{
    [SectionHeader("General Settings")]
    [RangeSlider(0, 100)]
    public Vector2 HP;

    [SectionHeader("General Settings")]
    [RangeSlider(0, 100)]
    public Vector2 Speed;

    [RangeSlider(0, 100)]
    public Vector2 Force;

    // То же название секции (чтобы увидеть, что заголовок может не дублироваться)
    [SectionHeader("General Settings")]
    [Button]
    private void Button()
    {
        Debug.Log("Button pressed!");
    }

    // Без SectionHeader: просто кнопка
    [Button]
    private void Button1()
    {
        Debug.Log("Button1 pressed!");
    }
}
