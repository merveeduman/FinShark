# 🦈 FinShark - Stock & Portfolio Management App

---

## 🇹🇷 Türkçe

### 📌 Projenin Amacı

Bu proje, modern web teknolojileri kullanılarak geliştirilmiş tam kapsamlı (full-stack) bir finans ve portföy yönetim uygulamasıdır.  
Amaç, kullanıcıların hisse senetlerini arayabildiği, finansal verileri inceleyebildiği ve kendi yatırım portföylerini yönetebildiği bir sistem oluşturmaktır.

Proje ile aşağıdaki temel hedefler sağlanmaktadır:

- Kullanıcı yönetimi (Authentication)
- Veri güvenliği (JWT tabanlı yetkilendirme)
- Gerçek zamanlı veri erişimi (External API)
- Kişisel portföy takibi
- Modern ve ölçeklenebilir yazılım mimarisi

---

### 🛠️ Kullanılan Teknolojiler

#### Backend
- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- ASP.NET Identity  
- JWT Authentication  

#### Frontend
- React (TypeScript)  
- Axios  
- React Router  
- Context API  
- React Toastify  

#### Harici Servis
- Financial Modeling Prep API  

---

### 🚀 Proje Ne Sağlar?

Uygulama aşağıdaki özellikleri içerir:

- Kullanıcı kayıt ve giriş sistemi  
- JWT tabanlı güvenli oturum yönetimi  
- Hisse senedi arama (TSLA, AAPL vb.)  
- Şirket finansal verilerini görüntüleme  
- Kullanıcıya özel portföy oluşturma  
- Portföye hisse ekleme ve silme  
- Modern ve kullanıcı dostu arayüz  

---

### ⚙️ Sistem Nasıl Çalışır?

#### 1) Kullanıcı Kayıt ve Giriş

- Kullanıcı frontend üzerinden kayıt olur  
- Bilgiler backend API’ye gönderilir  
- Backend kullanıcıyı veritabanına kaydeder  
- Login sırasında JWT token üretilir  
- Token frontend’de saklanır (localStorage)  

---

#### 2) Authentication Süreci

- Kullanıcı login olduğunda JWT token alır  
- Bu token her API isteğinde Authorization header ile gönderilir  
- Backend token’ı doğrular  
- Yetkili işlemlere erişim sağlanır  

---

#### 3) Hisse Arama Sistemi

- Kullanıcı arama kutusuna hisse sembolü girer (örn: TSLA)  
- Frontend, Financial Modeling Prep API’ye istek atar  
- API’den gelen sonuçlar kullanıcıya gösterilir  

---

#### 4) Portföy Yönetimi

- Kullanıcı istediği hisseyi portföyüne ekler  
- Bu işlem backend API’ye gönderilir  
- Veriler veritabanında kullanıcıya özel saklanır  
- Kullanıcı portföyünden hisse silebilir  

---
