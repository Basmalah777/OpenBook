using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;          // الكائن الذي تدور حوله الكاميرا
    public float rotationSpeed = 40f; // سرعة الدوران
    public float initialDistance = 5f; // المسافة الابتدائية بين الكاميرا والهدف
    public float finalDistance = 1f;  // المسافة النهائية عند الاقتراب
    public float tiltAngle = 40f;     // زاوية الميلان

    private float currentAngle = 0f;   // الزاوية الحالية
    private float currentDistance;    // المسافة الحالية
    private bool isReturningToStart = false; // حالة العودة إلى نقطة البداية
    private bool isZoomingIn = false; // حالة الاقتراب
    private bool isFinalRotation = false; // حالة الدوران الأخير

    void Start()
    {
        currentDistance = initialDistance; // تعيين المسافة الابتدائية
    }

    void Update()
    {
        if (target == null) return;

        if (!isReturningToStart && !isZoomingIn && !isFinalRotation)
        {
            // الدوران الأول
            currentAngle += rotationSpeed * Time.deltaTime;

            if (currentAngle >= 360f)
            {
                currentAngle = 0f; // العودة إلى نقطة البداية
                isReturningToStart = true; // تفعيل العودة
            }
        }
        else if (isReturningToStart)
        {
            // العودة إلى نقطة البداية
            currentAngle = Mathf.MoveTowards(currentAngle, 0f, rotationSpeed * Time.deltaTime);

            if (currentAngle == 0f)
            {
                isReturningToStart = false;
                isZoomingIn = true; // تفعيل الاقتراب
            }
        }
        else if (isZoomingIn)
        {
            // الاقتراب من الهدف
            currentDistance = Mathf.MoveTowards(currentDistance, finalDistance, Time.deltaTime * rotationSpeed);

            if (currentDistance == finalDistance)
            {
                isZoomingIn = false;
                isFinalRotation = true; // تفعيل الدوران الأخير
            }
        }
        else if (isFinalRotation)
        {
            // الدوران الأخير
            currentAngle += rotationSpeed * Time.deltaTime;

            if (currentAngle >= 360f)
            {
                currentAngle = 360f; // إيقاف الدوران عند إتمام دورة واحدة
                isFinalRotation = false; // التوقف
            }
        }

        // حساب الموقع الجديد للكاميرا
        Vector3 offset = new Vector3(
            Mathf.Sin(currentAngle * Mathf.Deg2Rad) * currentDistance,
            currentDistance * Mathf.Tan(tiltAngle * Mathf.Deg2Rad),
            Mathf.Cos(currentAngle * Mathf.Deg2Rad) * currentDistance
        );

        // تعيين الموقع الجديد
        transform.position = target.position + offset;

        // جعل الكاميرا تنظر دائمًا إلى الهدف
        transform.LookAt(target.position);
    }
}