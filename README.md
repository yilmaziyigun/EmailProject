# 📧 EmailProject - Modern Email Management System
Bu proje, yüksek performanslı .NET Core altyapısı ve modern Tailwind CSS arayüzü ile geliştirilmiş, uçtan uca bir e-posta gönderim ve yönetim panelidir.

##🚀 Proje Hakkında
EmailProject, kurumsal veya bireysel ihtiyaçlar için ölçeklenebilir bir e-posta çözümüdür. Arka planda asenkron işleme yeteneklerini kullanırken, ön yüzde kullanıcı deneyimini (UX) önceliklendiren şık bir tasarım sunar.

##🛠️ Teknik Yığın (Tech Stack)
Backend
Framework: .NET Core 8.0

 ORM: Entity Framework Core

 Email Library: MailKit / MimeKit

 Architecture: Repository Pattern & Service Layer (Clean Architecture prensipleri)

## Frontend
Styling: Tailwind CSS (Utility-first CSS)

## Interactivity: Razor Pages / MVC Views

## Icons: Heroicons / FontAwesome

## ✨ Temel Özellikler
✅ Asenkron Gönderim: async/await yapısı ile UI bloklanmadan mail gönderimi.

📱 Responsive Tasarım: Mobil, tablet ve masaüstü cihazlarla %100 uyumlu Tailwind arayüzü.

📄 Dinamik HTML Şablonları: Özelleştirilebilir ve parametrik e-posta içerikleri.

🛡️ Güvenli Yapı: SMTP kimlik bilgilerinin User Secrets veya environment variables ile korunması.

📊 Loglama: Başarılı ve hatalı gönderimlerin takibi için detaylı raporlama.

## 📂 Proje Yapısı
EmailProject.Web: Kullanıcı arayüzü ve Controller katmanı.

EmailProject.Business: İş mantığı ve email servisleri.

EmailProject.DataAccess: Veritabanı işlemleri ve Repository yapıları.

EmailProject.Entity: Veritabanı modelleri.
