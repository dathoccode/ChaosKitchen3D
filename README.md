# 🍳 Chaos Kitchen 3D

**Chaos Kitchen 3D** là tựa game mô phỏng nấu ăn góc nhìn thứ ba (lấy cảm hứng từ *Overcooked!*) được phát triển bằng **Unity 6 (URP)**. Trong game, người chơi vào vai một đầu bếp bận rộn, di chuyển trong gian bếp, sơ chế nguyên liệu, nấu nướng, bày biện món ăn lên đĩa và giao hàng đúng theo thực đơn yêu cầu trước khi hết giờ.

## 🎬 Demo Video
- Xem video demo: [YouTube Demo](https://youtu.be/bE-OQJfp-BI)
---

## ✨ Các chức năng chính (Key Features)

### 1. Điều khiển nhân vật & Vật lý (Player Movement & Physics)
- **Di chuyển 3D mượt mà:** Điều khiển nhân vật di chuyển 8 hướng kèm nội suy góc xoay (`Vector3.Slerp`).
- **Xử lý va chạm thông minh:** Sử dụng `Physics.CapsuleCast` giúp nhân vật trượt dọc theo cạnh bàn bếp khi di chuyển chéo vào vật cản thay vì bị khựng lại.
- **Nhận diện tương tác:** Sử dụng `Physics.Raycast` theo hướng di chuyển gần nhất để tự động chọn và làm nổi bật (highlight) quầy bếp phía trước (`SelectedCounterVisual`).

### 2. Hệ thống Quầy bếp đa dạng (Modular Counter System)
Kế thừa từ lớp cơ sở `BaseCounter`, mỗi loại quầy đảm nhận một chức năng riêng biệt trong dây chuyền nấu nướng:
- **Container Counter (Thùng chứa nguyên liệu):** Lấy các nguyên liệu thô (cà chua, phô mai, bắp cải, bánh mì, thịt sống,...) kèm hiệu ứng mở nắp thùng.
- **Clear Counter (Quầy trống):** Đặt, lấy nguyên liệu hoặc kết hợp nguyên liệu trực tiếp lên đĩa.
- **Cutting Counter (Quầy sơ chế/thớt):** Cắt nhỏ nguyên liệu dựa trên công thức (`CuttingRecipeSO`), hiển thị thanh tiến trình (`ProgressBarUI`) và hoạt ảnh dao cắt.
- **Stove Counter (Bếp nướng/rán):** Hoạt động theo State Machine (`Idle` → `Frying` → `Burning` → `Burned`). Nấu chín thịt dựa trên `FryingRecipeSO` và cảnh báo cháy khét nếu để quá thời gian theo `BurningRecipeSO`, đi kèm hiệu ứng hạt (Particle) và âm thanh xèo xèo.
- **Plate Counter (Quầy đĩa):** Tự động sinh đĩa sạch theo chu kỳ thời gian với số lượng giới hạn trên mặt quầy.
- **Delivery Counter (Quầy giao món):** Nơi người chơi nộp đĩa thức ăn hoàn chỉnh để kiểm tra đơn hàng.
- **Trash Counter (Thùng rác):** Hủy bỏ nguyên liệu hoặc món ăn bị chế biến sai/cháy.

### 3. Hệ thống Lắp ráp Món ăn & Đơn hàng (Recipe & Delivery System)
- **Lắp ráp nguyên liệu trên đĩa (`Plate`):** Kiểm tra danh sách nguyên liệu hợp lệ, ngăn trùng lặp nguyên liệu và hiển thị trực quan mô hình 3D hoàn chỉnh (`PlateCompleteVisual`) cùng các biểu tượng nguyên liệu (`PlateIconUI`).
- **Quản lý đơn hàng (`DeliveryManager`):** Tự động sinh ngẫu nhiên các đơn hàng từ thực đơn (`MenuSO` / `RecipeSO`) theo thời gian, đối chiếu nguyên liệu trên đĩa khi giao và thống kê số đơn hoàn thành.

### 4. Vòng đời Trò chơi & Quản lý Trạng thái (Game Flow & State Machine)
- **Luồng trạng thái (`GameManager`):** Điều phối các giai đoạn `Ready` → `CountdownToStart` (đếm ngược 3-2-1) → `Playing` (đồng hồ đếm ngược thời gian màn chơi) → `GameOver` (tổng kết số món đã giao).
- **Hệ thống chuyển cảnh (`Loader`):** Chuyển đổi giữa `MainMenuScene`, `LoadingScene` và `GameScene`, kết hợp `StaticDataManager` để dọn dẹp các sự kiện tĩnh (`static event`) khi tải lại màn chơi.

### 5. Giao diện, Âm thanh & Tùy chỉnh Điều khiển (UI, Audio & Rebinding)
- **Hệ thống UI trực quan:** Giao diện danh sách đơn hàng chờ (`DeliveryManagerUI`), đồng hồ thời gian (`GamePlayingClockUI`), thanh tiến độ hướng về camera (`LookAt`), màn hình Tạm dừng (`GamePauseUI`), Cài đặt (`OptionsUI`) và Kết thúc (`GameOverUI`).
- **Tùy chỉnh phím bấm (Key Rebinding):** Hỗ trợ cả **Bàn phím (Keyboard)** và **Tay cầm (Gamepad)**. Cho phép người chơi đổi phím trực tiếp trong menu Options và lưu cấu hình vào `PlayerPrefs` dưới dạng JSON.
- **Quản lý Âm thanh (`SoundManager` & `MusicManager`):** Phát hiệu ứng âm thanh 3D theo vị trí sự kiện (tiếng bước chân, chặt rau, rán thịt, nhặt/thả đồ, giao hàng thành công/thất bại) và nhạc nền; hỗ trợ tùy chỉnh âm lượng lưu trữ qua `PlayerPrefs`.

---

## 🛠️ Công nghệ & Kiến trúc sử dụng (Technologies & Architecture)

### Công nghệ cốt lõi (Tech Stack)
| Thành phần | Công nghệ / Package |
| :--- | :--- |
| **Game Engine** | Unity 6 (`6000.3.6f1`) |
| **Ngôn ngữ lập trình** | C# |
| **Render Pipeline** | Universal Render Pipeline (URP `17.3.0`) |
| **Hệ thống điều khiển** | Unity New Input System (`com.unity.inputsystem 1.18.0`) |
| **Camera** | Unity Cinemachine (`com.unity.cinemachine 3.1.7`) |
| **Giao diện (UI)** | Unity uGUI (`com.unity.ugui 2.0.0`) & TextMeshPro |
| **Lưu trữ dữ liệu cục bộ** | Unity `PlayerPrefs` (cài đặt âm lượng & cấu hình phím bấm) |

### Kiến trúc & Design Patterns nổi bật
- **Data-Driven Design với `ScriptableObject`:** Tách biệt hoàn toàn dữ liệu game (nguyên liệu `KitchenObjectSO`, công thức cắt `CuttingRecipeSO`, công thức nấu/cháy `FryingRecipeSO`/`BurningRecipeSO`, thực đơn `MenuSO`, âm thanh `AudioClipRefSO`) khỏi logic code, giúp dễ dàng mở rộng món ăn mới trực tiếp trên Unity Inspector.
- **Observer Pattern (`C# Events` & `EventHandler`):** Tách rời (decouple) hoàn toàn tầng Logic (Gameplay) với tầng Hiển thị (Visual/UI) và Âm thanh (Sound). UI và Animator chỉ lắng nghe sự kiện từ các Manager/Counter để cập nhật.
- **OOP - Kế thừa & Đa hình (Inheritance & Polymorphism):**
  - Lớp trừu tượng `BaseCounter` định nghĩa giao diện tương tác chung (`Interact`, `InteractAlternate`) cho toàn bộ quầy bếp.
  - Interface `IKitchenObjectParent` giúp cả `Player` và `BaseCounter` đều có thể sở hữu và trao đổi `KitchenObject` theo cùng một cơ chế.
  - Interface `IHasProgress` chuẩn hóa việc hiển thị `ProgressBarUI` cho mọi đối tượng có tiến trình (như `CuttingCounter`, `StoveCounter`).
- **State Machine Pattern:** Quản lý luồng trạng thái rõ ràng trong `GameManager` và `StoveCounter`.
- **Singleton Pattern:** Quản lý truy cập tập trung cho các hệ thống toàn cục (`GameManager`, `DeliveryManager`, `GameInput`, `SoundManager`, `MusicManager`, `Player`).

---

## 📁 Cấu trúc thư mục (Project Structure)

```text
Assets/
├── _Assets/                  # Models, Materials, Animations, Textures, AudioClips, Shaders
├── Prefab/                   # Prefab của Quầy bếp, Nguyên liệu (KitchenObject), Hiệu ứng và UI
├── Scenes/                   # Các scene: MainMenuScene, LoadingScene, GameScene
├── ScriptableObjects/        # Tài nguyên dữ liệu (KitchenObjectSO, RecipeSO, MenuSO, AudioClipRefSO,...)
├── Settings/                 # Cấu hình URP (Universal Render Pipeline) & Input Settings
└── Script/                   # Mã nguồn C# chính của dự án
    ├── Counter/              # Logic các loại quầy bếp (BaseCounter, StoveCounter, CuttingCounter,...)
    │   └── Visual/           # Xử lý hiệu ứng hình ảnh & hoạt ảnh của từng loại quầy
    ├── Interfaces/           # Các interface (IKitchenObjectParent, IHasProgress)
    ├── KitchenObject/        # Logic đối tượng nguyên liệu, đĩa (Plate) và hiển thị món hoàn chỉnh
    ├── Manager/              # Các lớp quản lý (GameManager, DeliveryManager, SoundManager, MusicManager,...)
    ├── Player/               # Điều khiển nhân vật, hoạt ảnh (PlayerAnimator) và âm thanh bước chân
    ├── ScriptableObject/     # Định nghĩa cấu trúc dữ liệu ScriptableObject
    ├── UI/                   # Toàn bộ script điều khiển giao diện người dùng
    └── UnSorted/             # Quản lý đầu vào (GameInput), chuyển cảnh (Loader), tiện ích Camera (LookAt)
```

---

## 🎮 Điều khiển mặc định (Default Controls)

> *Người chơi có thể tùy chỉnh lại toàn bộ phím bấm trong mục **Options** của trò chơi.*

| Hành động | Bàn phím (Keyboard) | Tay cầm (Gamepad) |
| :--- | :--- | :--- |
| **Di chuyển** | `W`, `A`, `S`, `D` | Cần gạt trái (Left Stick) |
| **Tương tác (Nhặt / Đặt / Nấu / Giao)** | `E` | Nút South (`A` Xbox / `Cross` PS) |
| **Tương tác phụ (Cắt / Sơ chế)** | `F` | Nút West (`X` Xbox / `Square` PS) |
| **Tạm dừng (Pause)** | `Esc` | Nút `Start` / `Options` |

---

