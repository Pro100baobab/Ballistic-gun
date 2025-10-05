## RT Physics Sandbox (Unity)

Small Unity sandbox to experiment with projectile motion, quadratic air drag, wind, and a controllable cannon with a live trajectory preview.

— Русская версия ниже —

### Features
- Cannon controller with independent muzzle elevation
- Real-time trajectory preview with air drag and wind
- Procedural projectile parameters (mass/radius) each shot
- Simple movement and yaw controls for the cannon base

### Controls
- W / S: Elevate / depress muzzle
- A / D: Strafe left / right (horizontal only)
- Q / E: Yaw (rotate base) left / right
- Space: Fire

### Requirements
- Unity 6000.0.54f1 (see `ProjectSettings/ProjectVersion.txt`)

### Getting started
1. Clone the repository.
2. Open the project in Unity 6000.0.54f1.
3. Open the sample scene (e.g., `Assets/Scenes/SampleScene.unity`).
4. Press Play.

### Project structure (selected)
- `Assets/Scripts/CannonController.cs`: Cannon movement, muzzle elevation, and firing
- `Assets/Scripts/TrajectoryRenderer.cs`: Trajectory preview logic
- `Assets/Scripts/QuadraticDrag.cs`: Per-projectile quadratic drag simulation
- `Assets/Target.prefab`, `Assets/CannotBall.prefab`: Sample prefabs

### Build
1. File → Build Settings…
2. Ensure the open scene is added to the build list
3. Choose target platform and Build

### License
See `LICENSE`.

---

## RT Physics Sandbox (Unity) — RU

Небольшой проект на Unity для экспериментов с полётом снаряда, квадратичным аэродинамическим сопротивлением, ветром и управляемой пушкой с предпросмотром траектории.

### Возможности
- Контроллер пушки с независимым подъёмом ствола
- Предпросмотр траектории в реальном времени с учётом сопротивления и ветра
- Случайные параметры снаряда (масса/радиус) для каждого выстрела
- Простое управление перемещением и поворотом основания пушки

### Управление
- W / S: Поднять / опустить ствол
- A / D: Движение влево / вправо (горизонтально)
- Q / E: Поворот (yaw) основания влево / вправо
- Space: Выстрел

### Требования
- Unity 6000.0.54f1 (см. `ProjectSettings/ProjectVersion.txt`)

### Как запустить
1. Клонируйте репозиторий.
2. Откройте проект в Unity 6000.0.54f1.
3. Откройте сцену `Assets/Scenes/SampleScene.unity`.
4. Нажмите Play.

### Структура (основные файлы)
- `Assets/Scripts/CannonController.cs`: Движение пушки, подъём ствола, выстрел
- `Assets/Scripts/TrajectoryRenderer.cs`: Логика предпросмотра траектории
- `Assets/Scripts/QuadraticDrag.cs`: Симуляция квадратичного сопротивления для снаряда
- `Assets/Target.prefab`, `Assets/CannotBall.prefab`: Примеры префабов

### Сборка
1. File → Build Settings…
2. Убедитесь, что сцена добавлена в список сборки
3. Выберите платформу и выполните Build

### Лицензия
См. `LICENSE`.

