# ⚓ NelayanGo
### Aplikasi desktop untuk pencatatan tangkapan harian tiap nelayan dengan penyediaan forecast cuaca perairan

## Kelompok 6
- Anggota 1: Ammar Ali Yasir (23/520644/TK/57406)
- Anggota 2: Muhammad Hafidz Al Farisi (23/519650/TK/57256)
- Anggota 3: Nicholas Shane Pangihutan Siahaan (23/520590/TK/57399)

## 📝 Deskripsi Proyek
NelayanGo adalah aplikasi *desktop* berbasis **C# WPF** yang dirancang untuk mendigitalisasi proses pencatatan hasil tangkapan harian bagi nelayan. Aplikasi ini terintegrasi dengan data cuaca perairan *real-time* untuk membantu nelayan mengoptimalkan jadwal dan keamanan pelayaran.

## ✨ Fitur Utama
1.  **Pencatatan Tangkapan Harian:** Input tangkapan harian dengan perhitungan harga otomatis bagi user.
2.  **Prakiraan Cuaca Perairan:** Perkiraan cuaca, kecepatan angin, dan suhu.
3.  **Manajemen Pengguna:** Login dan register bagi user dan adanya role admin untuk harga ikan.
4.  **Laporan Sederhana:** Dashboard dengan statistik tangkapan dan riwayat tangkapan.

---

## 🛠️ Teknologi yang Digunakan (Tech Stack)

| Kategori | Teknologi | Keterangan |
| :--- | :--- | :--- |
| **Frontend** | C# WPF (.NET Framework) | Digunakan untuk membangun antarmuka aplikasi desktop (UI) yang kaya dan *responsive*. |
| **Backend/Logika** | C# | Implementasi penuh dari semua logika PBO (Kelas `Nelayan`, `Tangkapan`, dll.). |
| **Database** | PostgreSQL | Sistem Database relasional yang andal, di-*host* melalui Supabase. |
| **API Layer** | Supabase | Menyediakan layanan *Backend-as-a-Service* (BaaS), termasuk autentikasi dan *RESTful API* untuk interaksi data *real-time*. |
| **API Pihak Ketiga** | OpenWeatherMap API | Sumber data eksternal untuk menyediakan data cuaca perairan. |

## 📐 Struktur Pemrograman Berorientasi Objek (PBO)

Proyek ini dibangun di atas prinsip PBO. Struktur utama terbagi menjadi lapisan **Model**, **View**, dan **Controller** (atau ViewModel dalam konteks WPF).

* **Implementasi Utama:**
    * **Class & Object:** Diterapkan pada model-model seperti pada HargaIkanModel.
    * **Inheritance:** Diterapkan pada pewarisan kelas seperti pada LoginWindow dengan Window.      
    * **Polymorphism:** Diterapkan pada event click dengan behavior yang berbeda untuk tiap window.
    * **Encapsulation:** Diterapkan pada getter dan setter seperti pada IkanTangkapanModel.
    * **Abstraction:** Diterapkan pada pemisahan UI dengan logika kompleks seperti pada HargaIkanDataService.

---

## 🚀 Instalasi & Setup

Untuk menjalankan aplikasi ini, ikuti langkah-langkah di bawah ini:

### Persyaratan Sistem
* Windows OS (Aplikasi WPF).
* .NET Framework 4.7.2 atau versi terbaru.
* Visual Studio (Disarankan).

### Langkah-Langkah Menjalankan Proyek

1.  **Clone Repository:**
    ```bash
    git clone https://github.com/MHafidzAlFarisi/NelayanGo.git
    cd NelayanGo
    ```
2.  **Buat Database:**
    Buat Database dengan kolom seperti di gambar:
    <img width="453" height="804" alt="image" src="https://github.com/user-attachments/assets/496840bf-46df-4f08-95b3-106f3a8da4b4" />
 
4.  **Konfigurasi Environment:**
    Buat file baru di direktori root bernama **`.env`** atau gunakan file `.env.example` yang tersedia. Isi dengan *credentials* API Anda:

    ```
    # Konfigurasi Supabase
    SUPABASE_URL=https://hnamnhkbtnvbowmreddz.supabase.co
    SUPABASE_KEY=[ANON PUBLIC KEY SUPABASE ANDA]
    SUPABASE_HOST=aws-1-ap-southeast-2.pooler.supabase.com
    SUPABASE_PORT=5432
    SUPABASE_USER=postgres.hnamnhkbtnvbowmreddz
    SUPABASE_PASSWORD=[PASSWORD SUPABASE ANDA]
    SUPABASE_DB=postgres

    # Konfigurasi OpenWeatherMap

    OPENWEATHER_API_KEY=[API KEY OPENWEATHERMAP ANDA]
    ```

5.  **Buka dan Compile:**
    * Buka file solusi (`NelayanGo.sln`) di Visual Studio.
    * Install NuGet Package GMap.NET.WinPresentation, Microsoft.Windows.Compatibility, OxyPlot.Wpf, dan supabase.
    * Pastikan semua *dependency* C# telah ter-*resolve*.
    * **Run** aplikasi (F5).

---

## 🔑 Informasi Akses Demo 

Untuk keperluan penilaian dan demonstrasi, Anda dapat menggunakan akun yang telah disediakan di bawah ini:

| Role | Username | Password | Keterangan |
| :--- | :--- | :--- | :--- |
| **Nelayan (User Biasa)** | **heinz** | `1` | Digunakan untuk *logbook* dan melihat *forecast* cuaca. |
| **Admin (Opsional)** | **admin** | `admin123` | Digunakan untuk menambah Harga Ikan. |

