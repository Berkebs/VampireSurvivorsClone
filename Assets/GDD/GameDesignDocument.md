# Game Design Document (GDD) - Vampire Survivors Clone

## 1. Oyun Özeti (Game Overview)
**Konsept:** Vampire Survivors, oyuncunun üzerine gelen yüzlerce düşmana karşı hayatta kalmaya çalıştığı, minimalist kontrollere sahip bir *rogue-lite time survival* oyunudur.
**Tür:** Rogue-lite, Shoot 'em up, Bullet Hell
**Platform:** PC / Mobil
**Hedef Kitle:** Casual ve Hardcore oyuncular (Easy to learn, hard to master)

## 2. Oynanış Mekanikleri (Gameplay Mechanics)

### 2.1. Temel Döngü (Core Loop)
1. **Savaş:** Oyuncu düşmanları öldürür.
2. **Topla:** Düşmanlardan düşen XP Gem'lerini toplar.
3. **Geliş:** Seviye atlar ve rastgele sunulan 3-4 güçlendirmeden (silah/pasif) birini seçer.
4. **Tekrarla:** Karakter güçlendikçe düşmanlar da güçlenir ve sayıları artar.
5. **Sonuç:** Oyuncu ölür (ve meta-coin kazanır) veya süreyi tamamlar (örn: 30 dk).

### 2.2. Kontroller (Controls)
- **Hareket:** WASD, Yön Tuşları veya Gamepad Sol Analog.
- **Saldırı:** Otomatik. Oyuncu saldırmaz, silahlar cooldown sürelerine göre otomatik ateşlenir.
- **Onay/Seçim:** Space veya Enter (UI etkileşimi için).

### 2.3. Kamera (Camera)
- **Perspektif:** Top-down (Kuş bakışı) 2D.
- **Davranış:** Oyuncuyu merkezde tutar. **Cinemachine** Virtual Camera kullanılacak.
- **Dead Zone:** Yok veya çok küçük, oyuncu ekranın ortasında kalmalı.

## 3. Karakterler ve İstatistikler (Characters & Stats)

### 3.1. Temel İstatistikler
- **Max Health:** Maksimum sağlık.
- **Recovery:** Saniye başına can yenileme.
- **Armor:** Alınan hasarı azaltma.
- **Move Speed:** Hareket hızı.
- **Might:** Verilen hasar çarpanı.
- **Area:** Saldırıların etki alanı büyüklüğü.
- **Speed (Projectile):** Mermi hızı.
- **Duration:** Silah etkilerinin süresi.
- **Cooldown:** Saldırılar arası bekleme süresi (düşük olması iyidir).
- **Luck:** Kritik vuruş şansı ve iyi eşya düşme ihtimali.

### 3.2. Örnek Başlangıç Karakteri
- **İsim:** Antonio (Örnek)
- **Başlangıç Silahı:** Whip (Kırbaç)
- **Özel Yetenek:** Her 10 seviyede %10 daha fazla hasar verir (Max %50).

## 4. Silahlar ve Eşyalar (Weapons & Items)

### 4.1. Silah Mekaniği
Silahlar otomatik çalışır. Her silahın kendine has bir davranışı vardır.
- **Whip (Kırbaç):** Yatay olarak öne (veya arkaya) vurur.
- **Magic Wand (Büyü Asası):** En yakın düşmana mermi atar.
- **Garlic (Sarımsak):** Oyuncunun etrafında dairesel hasar alanı oluşturur.
- **Axe (Balta):** Yukarı doğru parabolik bir rotada fırlatılır.

### 4.2. Pasif Eşyalar
Silah slotlarından ayrı olarak pasif slotları vardır.
- **Spinach:** Hasarı artırır.
- **Armor:** Alınan hasarı azaltır.
- **Empty Tome:** Cooldown süresini azaltır.

### 4.3. Silah Evrimi (Evolution)
Bir silah max seviyeye (örn. Lvl 8) ulaştığında ve uyumlu pasif eşya (Lvl 1 olması yeterli) envanterde varsa, bir sandıktan (Chests) "Evrimleşmiş Silah" çıkar.
- *Örnek:* Whip + Hollow Heart = Bloody Tear (Can çalan kırbaç).

## 5. Düşmanlar ve Yapay Zeka (Enemies & AI)

### 5.1. Davranış
- Düşmanların tek amacı oyuncunun pozisyonuna doğru yürümektir. Karmaşık pathfinding (NavMesh) yerine basit vektör takibi performans için daha iyidir (binlerce düşman olacağı için).
- **"Boids" veya "Steering Behaviors"** benzeri bir yapı ile birbirlerinin içinden geçmeleri engellenebilir (Soft Collision).

### 5.2. Düşman Tipleri
- **Fodder (Yem):** Zayıf, yavaş, çok sayıda (Örn: Yarasalar).
- **Tank:** Yavaş, çok canı var, az sayıda.
- **Rusher:** Hızlı, oyuncuya depar atar.
- **Boss:** Büyük sprite, çok yüksek can, öldüğünde sandık düşürür.

### 5.3. Spawning (Canavarların Doğuşu)
- Düşmanlar kamera görüş alanının hemen dışında (off-screen) doğar.
- Zamanla spawn oranı (rate) artar.
- Belirli dakikalarda (örn: 5:00, 10:00) büyük dalgalar (swarm) veya Boss gelir.

## 6. Harita ve Çevre (Level Design)

### 6.1. Infinite Tiling Background
- Oyuncu sonsuza kadar bir yöne gidebilir.
- Zemin sprite'ları (tile) oyuncu ilerledikçe dinamik olarak yer değiştirir.

### 6.2. Yıkılabilir Objeler
- Meşaleler, şamdanlar. Kırıldığında altın veya can iksiri (tavuk) düşer.

## 7. Kullanıcı Arayüzü (UI)

### 7.1. HUD (Heads-Up Display)
- Sol Üst: HP Barı.
- Üst Orta: Sayaç (Hayatta kalınan süre).
- Sağ Üst: Öldürülen düşman sayısı (Kill Count).
- Üst: XP Barı.
- Alt/Yanlar: Mevcut silahlar ve pasifler (ikonlar).

### 7.2. Menüler
- **Main Menu:** Start, PowerUps, Collection, Settings.
- **Level Up Screen:** 3 veya 4 seçenekli kart yapısı. Oyun durur (Time Scale = 0).
- **Pause Menu:** İstatistikleri gösterir.
- **Game Over:** Sonuç ekranı (Total Time, Gold Earned, DPS tablosu).

## 8. Teknik Notlar

### 8.1. Performans Optimizasyonu
- **Object Pooling:** Düşmanlar, mermiler, XP gemleri, hasar textleri (damage numbers) kesinlikle havuz sistemiyle yönetilmeli. Instantiate/Destroy kullanılmamalı.
- **Sprite Animation:** Mümkünse Animator yerine basit sprite swap scriptleri veya GPU instancing teknikleri.

### 8.2. Unity Paketleri
- **Input System:** Yeni nesil input sistemi.
- **Cinemachine:** Kamera kontrolü, shake efektleri.
- **TextMeshPro:** Tüm yazılar için.
