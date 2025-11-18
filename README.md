# 🎮 Uçan Obje Oyunu — WPF OOP Game

Bu proje, **WPF**, **C#** ve **.NET 9** kullanılarak geliştirilmiş basit bir refleks oyunudur.  
Oyuncu, ekranda sağdan gelen engellerden kaçmak için **zıplama** hareketi yapar. Oyun;  
**GameEngine**, **Player**, **Obstacle** gibi sınıflarla tasarlanmış olup,  
nesne yönelimli programlama (OOP) prensiplerinin gerçek bir uygulamasını içerir.

---



## 🚀 Özellikler

- ✔ **OOP temelli mimari**
  - GameObject → Player & Obstacle miras yapısı
  - Ayrıştırılmış GameEngine mantığı
- ✔ **Gerçek zamanlı oyun döngüsü** (DispatcherTimer ~60 FPS)
- ✔ **Çarpışma algılama** (Rectangle intersects)
- ✔ **Dinamik engel üretimi**
- ✔ **Skor sistemi**
- ✔ **Event tabanlı yapılar**
  - `ScoreUpdated`
  - `CollisionDetected`
  - `GameOver`
- ✔ **WPF Canvas üzerinde rendering**
- ✔ **Basit kullanıcı dostu arayüz**

---

## 🧱 Mimari Yapı
UcanObjeOyunu
│
├── Models
│   ├── GameObject.cs     → Player & Obstacle için ortak özellikler
│   ├── Player.cs         → Zıplama, yer çekimi, hareket mantığı
│   └── Obstacle.cs       → Sağdan gelen engeller
│
├── GameEngine.cs         → Oyun döngüsü, çarpışma kontrolleri, skor, event yönetimi
├── MainWindow.xaml       → UI tasarımı (Canvas bazlı)
└── MainWindow.xaml.cs    → UI ↔ GameEngine entegrasyonu
🎮 Kontroller
Tuş	İşlev
SPACE	"B" tuşu
ENTER	Oyunu başlatır / yeniden başlatır
🛠 Çalıştırma
1️⃣ Visual Studio ile

Projeyi aç

Üstten Start / Başlat

Oyun pencereniz açılır

2️⃣ .NET CLI (Komut Satırı) ile
dotnet run
🧠 Bu Projede Öğrenilenler

WPF Canvas üzerinde oyun mekaniği tasarlama

DispatcherTimer ile game loop oluşturma

Player–Obstacle–GameEngine yapıları ile OOP mantığını uygulama

Event’lerle (ScoreUpdated, GameOver…) UI ve oyun motorunu ayırma

Çarpışma algılama mantığı (intersecting rectangles)

🔮 Yol Haritası (To-Do)

 Ana menü ekranı ekleme

 Oyun durdurma / devam ettirme (Pause)

 Arka plan müziği ve ses efektleri

 Farklı zorluk seviyeleri

 Yeni engel türleri (üstten gelen, hareketli engeller vs.)

 En yüksek skor kaydı (local file)

 Oyuncu için karakter/sprite değişimi



