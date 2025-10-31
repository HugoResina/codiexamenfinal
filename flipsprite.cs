  [SerializeField] private bool flipX = false;  // Invierte el sprite horizontalmente
    [SerializeField] private bool flipY = false;  // Invierte el sprite verticalmente
    [SerializeField] private bool smoothRotation = false; // Suavizar la rotación
    [SerializeField] private float rotationSpeed = 10f; // Velocidad de suavizado

    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
               Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

              Vector2 direction = (mouseWorldPos - transform.position).normalized;

               float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

              Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f); // -90 si tu sprite mira hacia "arriba" por defecto

              if (smoothRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = targetRotation;
        }

       
        if (flipX || flipY)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (flipX && direction.x < 0 ? -1 : 1);
            scale.y = Mathf.Abs(scale.y) * (flipY && direction.y < 0 ? -1 : 1);
            transform.localScale = scale;
        }
    }