
# ⚓ NelayanGo

### Aplikasi desktop untuk pencatatan tangkapan harian tiap nelayan dengan penyediaan forecast cuaca perairan

## Kelompok 6
- Anggota 1: Ammar Ali Yasir (23/520644/TK/57406)
- Anggota 2: Muhammad Hafidz Al Farisi (23/519650/TK/57256)
- Anggota 3: Nicholas Shane Pangihutan Siahaan (23/520590/TK/57399)
<img width="1253" height="934" alt="Untitled Diagram-Class Diagram drawio (1)" src="https://github.com/user-attachments/assets/090ce736-125d-440a-803e-fdf4e3a015ac" />

## 📝 Deskripsi Proyek
NelayanGo adalah aplikasi *desktop* berbasis **C# WPF** yang dirancang untuk mendigitalisasi proses pencatatan hasil tangkapan harian bagi nelayan. Aplikasi ini terintegrasi dengan data cuaca perairan *real-time* untuk membantu nelayan mengoptimalkan jadwal dan keamanan pelayaran.

## ✨ Fitur Utama
1.  **Pencatatan Tangkapan Harian:** Input data jenis ikan, berat, dan lokasi tangkapan per sesi pelayaran.
2.  **Prakiraan Cuaca Perairan:** Integrasi dengan OpenWeatherMap API untuk menampilkan *forecast* cuaca, kecepatan angin, dan suhu di area tangkapan.
3.  **Manajemen Pengguna:** Modul Login dan Logout yang terintegrasi dengan sistem autentikasi Supabase.
4.  **Laporan Sederhana:** Tampilan riwayat tangkapan yang memudahkan nelayan memonitor profitabilitas.

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

* **Implementasi Utama:** (Merujuk pada Class Diagram yang Anda unggah)
    * **Inheritance:** Terdapat kelas **`User`** yang diwarisi oleh kelas **`Nelayan`** dan **`Admin`** (jika ada).
    * **Encapsulation:** Penggunaan *properties* dengan *access modifier* `private` untuk melindungi data sensitif (misal: *connection string*, *password hash*).
    * **Abstraction:** Menggunakan *Interfaces* (misal: `IDataService`) untuk menjamin *loosely coupled code* antar komponen.

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
2.  **Konfigurasi Environment:**
    Buat file baru di direktori root bernama **`.env`** atau gunakan file `.env.example` yang tersedia. Isi dengan *credentials* API Anda:

    ```
    # Konfigurasi Supabase
    SUPABASE_URL=https://hnamnhkbtnvbowmreddz.supabase.co
    SUPABASE_KEY=[ANON PUBLIC KEY SUPABASE ANDA]

    # Konfigurasi OpenWeatherMap
    OPENWEATHER_API_KEY=[API KEY OPENWEATHERMAP ANDA]
    ```

3.  **Buka dan Compile:**
    * Buka file solusi (`NelayanGo.sln`) di Visual Studio.
    * Pastikan semua *dependency* C# telah ter-*resolve*.
    * **Run** aplikasi (F5).

---

## 🔑 Informasi Akses Demo 

Untuk keperluan penilaian dan demonstrasi, Anda dapat menggunakan akun yang telah disediakan di bawah ini:

| Role | Username | Password | Keterangan |
| :--- | :--- | :--- | :--- |
| **Nelayan (User Biasa)** | **heinz** | `1` | Digunakan untuk *logbook* dan melihat *forecast* cuaca. |
| **Admin (Opsional)** | **admin** | `admin123` | Digunakan untuk [Jelaskan Fungsi Admin, misal: melihat seluruh data tangkapan]. |

