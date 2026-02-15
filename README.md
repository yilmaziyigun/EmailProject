# 📧 YılmazMail – Modern Mail Uygulaması

Modern, koyu temalı, kullanıcı dostu ve gerçek SMTP entegrasyonuna sahip bir mail uygulaması.
ASP.NET Core MVC ve Tailwind CSS kullanılarak geliştirilmiştir.

## 🚀 Proje Hakkında

YılmazMail, Gmail benzeri kullanıcı deneyimi sunan, modern arayüzlü ve güvenli bir mail yönetim sistemidir.
Bu proje yalnızca CRUD mantığında bir demo değil; gerçek SMTP ile çalışan ve email doğrulama mekanizmasına sahip production-ready bir mimariye sahiptir.

## 🔐 Gelişmiş Kimlik Doğrulama Sistemi

Uygulamada gerçek SMTP entegrasyonu bulunmaktadır.

## 📩 Email Doğrulama Süreci

Kullanıcı kayıt olur.
Sistem 6 haneli rastgele bir doğrulama kodu üretir.
Kod veritabanına kaydedilir (ConfirmCode).
Aynı kod gerçek SMTP üzerinden kullanıcının mail adresine gönderilir.
Kullanıcı gelen kodu girer.
Eğer:  
Girilen kod == Veritabanındaki ConfirmCode → EmailConfirmed = true yapılır.  
Aksi durumda giriş yapılamaz.

✔ Email doğrulanmadan sisteme giriş yapılamaz.  
✔ Güvenli ve kontrollü authentication süreci uygulanır.

## ✨ Özellikler

- 🔐 Kimlik doğrulama sistemi (6 haneli email doğrulama)
- 📥 Gelen kutusu yönetimi
- 📤 Giden kutusu
- ⭐ Yıldızlı mesajlar
- 🗑 Çöp kutusu
- 🔎 Mesaj arama
- 📝 Rich Text Editor (Quill.js)
- 👤 Profil yönetimi
- 🌙 Modern koyu tema (Tailwind CSS)
- 📧 Gerçek SMTP entegrasyonu


## 🛠 Kullanılan Teknolojiler

ASP.NET Core MVC
Entity Framework Core
ASP.NET Identity
SQL Server
Tailwind CSS
Quill.js (Rich Text Editor)
SMTP (MailKit / System.Net.Mail)

## 📸 Ekran Görüntüleri

### 🔐 Login Ekranı
<img width="1883" height="875" alt="login" src="https://github.com/user-attachments/assets/006e3017-8a25-4f09-a487-c896708e26a2" />

### 📥 Gelen Kutusu
<img width="1884" height="884" alt="inbox" src="https://github.com/user-attachments/assets/4d977a06-1268-4ec1-88c5-79905b50e1ef" />

### 📤 Giden Kutusu
<img width="1890" height="859" alt="sendbox" src="https://github.com/user-attachments/assets/844496ae-c258-431f-8bb1-d3a1b5b2c740" />

### ⭐ Yıldızlı Mesajlar
<img width="1892" height="832" alt="yıldızlı" src="https://github.com/user-attachments/assets/757736e2-bede-493a-9e87-8547fdb36e72" />

### ✉️ Mesaj Gönderme
<img width="1267" height="838" alt="mail gönder" src="https://github.com/user-attachments/assets/2e3e9875-8d5c-4775-a73a-c7498203366b" />

## 🧩 Modül Detayları
📥 Inbox Sistemi
Okunmamış mesajlar kalın punto ile gösterilir
Mesaj detayı modal ile açılır
Tarih bilgisi gösterimi
Yıldız ekleme / kaldırma
Arama fonksiyonu

## ✉️ Mesaj Gönderme

Rich text desteği (bold, italic, list, link)
HTML içerik kaydetme
Modern compose modal tasarımı
Gerçek SMTP ile mail gönderimi

## 👤 Profil Yönetimi

Profil fotoğrafı yükleme
Kullanıcı adı güncelleme
Şifre değiştirme
Email doğrulama kontrolü

## 🧠 Mimari Yapı

Katmanlı mimari
Entity tabanlı modelleme
ASP.NET Identity ile kullanıcı yönetimi
Authorization kontrolü (Mesaj detayı sadece alıcı veya gönderici tarafından görüntülenebilir)


## 🔒 Güvenlik

Email doğrulaması zorunludur
ConfirmCode veritabanında saklanır
Identity tabanlı authentication
Mesaj erişim yetki kontrolü
Yetkisiz kullanıcı erişimi engellenir

## 🎨 UI & UX

Tailwind CSS ile modern koyu tema
Responsive tasarım
Gmail benzeri kullanıcı deneyimi
Minimal ve temiz arayüz


### 👨‍💻 Yılmaz İyigün

⭐ Eğer projeyi beğendiysen yıldız bırakmayı unutma!
