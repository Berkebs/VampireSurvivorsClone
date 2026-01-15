# Development Roadmap - Vampire Survivors Clone

Bu yol haritası, projenin sıfırdan "Oynanabilir Beta" sürümüne kadar olan geliştirme sürecini kapsar.

## Milestone 1: Prototip (Core Gameplay)
**Hedef:** Basit bir kare karakterin hareket ettiği, tek bir düşmanın olduğu ve bir silahın çalıştığı minimal versiyon.
- [ ] **Proje Kurulumu:** Unity projesi, Git repo, klasör yapısı (Scripts, Prefabs, Sprites).
- [ ] **Oyuncu Kontrolcüsü:** Yeni Input System ile 8 yönlü hareket.
- [ ] **Kamera:** Cinemachine Virtual Camera ile oyuncu takibi.
- [ ] **Infinite Map:** Sonsuz tekrarlayan zemin (Tilemap veya Sprite takibi).
- [ ] **İlk Silah (Garlic/Sarımsak):** Oyuncunun etrafında bir hasar alanı (Collider).
- [ ] **Test Düşmanı:** Oyuncuya doğru düz bir çizgide gelen basit küp/sprite.
- [ ] **Hasar ve Ölüm:** Düşman öldüğünde yok olması.

## Milestone 2: Alpha (Basic Game Loop & Spawners)
**Hedef:** Oyunun bir döngüye girmesi, düşman dalgaları ve seviye atlama temelleri.
- [ ] **Object Pooling Sistemi:** Düşmanlar ve mermiler için havuz yapısı.
- [ ] **Enemy Spawner:** Kamera dışında sürekli düşman üreten sistem.
- [ ] **XP Gemleri:** Düşmanlar ölünce XP düşürmesi ve oyuncunun bunları toplaması (Magnet etkisi).
- [ ] **Level Up Mantığı:** XP barı dolunca oyunun durması (UI yok, sadece log).
- [ ] **Health System:** Oyuncunun canı ve hasar alması (basit UI barı).
- [ ] **Game Over:** Oyuncu ölünce oyunun bitmesi.

## Milestone 3: Progression & UI (İlerleme ve Arayüz)
**Hedef:** Oyuncunun seçim yapabilmesi ve oyunun görselleşmesi.
- [ ] **Level Up UI:** 3 Rastgele kart seçeneğinin ekrana gelmesi.
- [ ] **Upgrade Manager:** Seçilen kartın (Silah/Pasif) envantere eklenmesi veya seviyesinin artması.
- [ ] **HUD:** Süre sayacı, Kill counter, Silah slotları göstergesi.
- [ ] **Main Menu:** Başlat butonu ve basit sahne geçişi.
- [ ] **Damage Numbers:** Düşmanların üzerinde hasar sayılarının çıkması (Pop-up text).

## Milestone 4: Content Expansion (İçerik Genişletmesi)
**Hedef:** Oyunun çeşitlendirilmesi.
- [ ] **Yeni Silahlar:** Magic Wand (En yakına mermi), Axe (Parabolik atış).
- [ ] **Düşman Çeşitliliği:** Hızlı koşanlar, Tanklar, menzilli düşmanlar.
- [ ] **Sprite & Animasyon:** Placeholder kareler yerine gerçek 2D karakter sprite'ları ve animasyonları.
- [ ] **Ses Efektleri:** Vuruş, seviye atlama, müzik.

## Milestone 5: Polish & Juice (Cila)
**Hedef:** Oyun hissinin (Game Feel) iyileştirilmesi.
- [ ] **Hit Stop / Screen Shake:** Vuruş hissi için milisaniyelik duraksamalar ve ekran titremesi.
- [ ] **VFX:** Ölüm efektleri, kan partikülleri, level up parlaması.
- [ ] **Kritik Vuruşlar:** Görsel olarak farklı hasar sayıları.
- [ ] **Performance Tuning:** Profiler ile kontroller ve optimizasyon.
