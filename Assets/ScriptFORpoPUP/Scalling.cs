using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scalling : MonoBehaviour
{
    float minScale = 0.03f;  // الحد الأدنى للحجم
    float maxScale = 1.5f;   // الحد الأقصى للحجم
    float scaleFactor = 1;   // معامل التكبير
    float scaleSpeed = 0.5f; // سرعة التكبير
    float slowScaleSpeed = 0.2f; // سرعة التصغير أبطأ من التكبير بكثير
    float floatAmplitude = 0.1f; // شدة تأثير الطفو (زيادة الشدة)
    float floatFrequency = 2f;    // تردد الطفو
    float floatMaxScaleAdd = 0.1f; // إضافة تكبير صغير فوق الحد الأقصى للحجم

    void Start()
    {
        scaleFactor = minScale; // تعيين الحجم الابتدائي
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) // تقليل الحجم عند الضغط على A
        {
            scaleFactor -= slowScaleSpeed * Time.deltaTime; // تصغير الحجم بسرعة بطيئة جداً
        }
        else if (Input.GetKey(KeyCode.D)) // زيادة الحجم عند الضغط على D
        {
            scaleFactor += scaleSpeed * Time.deltaTime; // زيادة الحجم بسرعة عادية
        }

        // ضبط الحجم ليبقى ضمن الحدود المسموح بها، مع إضافة تكبير إضافي عند الحد الأقصى
        scaleFactor = Mathf.Clamp(scaleFactor, minScale, maxScale + floatMaxScaleAdd);

        // إضافة تأثير الطفو عند الوصول للحد الأقصى
        float floatEffect = 0f;
        if (Mathf.Approximately(scaleFactor, maxScale + floatMaxScaleAdd))
        {
            floatEffect = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        }

        // تطبيق الحجم مع تأثير الطفو
        transform.localScale = Vector3.one * (scaleFactor + floatEffect);
    }
}