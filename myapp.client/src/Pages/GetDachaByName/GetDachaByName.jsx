import React, { useState, useEffect } from 'react';
import './GetDachaByName.css';

function GetDachaByName() {
    const [dachaName, setDachaName] = useState('');
    const [loading, setLoading] = useState(false);
    const [dacha, setDacha] = useState(null);
    const [debouncedDachaName, setDebouncedDachaName] = useState('');

    useEffect(() => {
        const handler = setTimeout(() => {
            setDebouncedDachaName(dachaName);
        }, 500);

        return () => {
            clearTimeout(handler);
        };
    }, [dachaName]);

    useEffect(() => {
        async function GetData() {
            if (!debouncedDachaName.trim()) {
                setDacha(null);
                setLoading(false);
                return;
            }

            setLoading(true);
            try {
                const replacedName = dachaName.replace(" ", "%20");
                const response = await fetch(`https://localhost:7019/api/MyDacha/${replacedName}`);

                if (response.ok) {
                    const data = await response.json();
                    setDacha(data);
                } else {
                    setDacha(null);
                }
            } catch (error) {
                console.error("Error fetching dacha by name:", error);
                setDacha(null);
            } finally {
                setLoading(false);
            }
        }

        GetData();
    }, [debouncedDachaName]);

    const handleInputChange = (event) => {
        setDachaName(event.target.value);
    }

    return (
        <div className='get-dacha-by-name-container'>
            <h1>Dacha haqida ma'lumot olish uchun dacha Nomini kiriting</h1>

            <input
                type="text"
                placeholder="Dacha Nomini kiriting..."
                value={dachaName}
                onChange={handleInputChange}
            />

            {loading && <div className='loading-message'>Yuklanmoqda...</div>}

            {!loading && dacha && (
                <div className='dacha-details'>
                    <h2>Dacha ma'lumotlari:</h2>
                    <div className='dacha-info-grid'>
                        <div className='dacha-info-item'>
                            <strong>ID:</strong> <span>{dacha.id}</span>
                        </div>
                        <div className='dacha-info-item'>
                            <strong>Nomi:</strong> <span>{dacha.name}</span>
                        </div>
                        <div className='dacha-info-item'>
                            <strong>Maydoni:</strong> <span>{dacha.sqft}</span>
                        </div>
                        <div className='dacha-info-item'>
                            <strong>Ijara olsa bo'ladimi?:</strong> <span>{dacha.isAvailable ? 'Ha' : 'Yo\'q'}</span>
                        </div>
                        <div className='dacha-info-item'>
                            <strong>Ball:</strong> <span>{dacha.rate}</span>
                        </div>
                        <div className='dacha-info-item'>
                            <strong>Qulayliklari:</strong> <span>{dacha.amenity}</span>
                        </div>
                        <div className='dacha-info-item'>
                            <strong>Yaratilgan vaqti:</strong> <span>{dacha.createdDate}</span>
                        </div>
                        <div className='dacha-info-item'>
                            <strong>Ohirgi o'zgartirish:</strong> <span>{dacha.updatedDate}</span>
                        </div>
                    </div>
                </div>
            )}

            {!loading && !dacha && debouncedDachaName && (
                <div className='not-found-error'>
                    <p>Berilgan Nom bo'yicha dacha topilmadi.</p>
                    <p>Bunday dacha bizda bo'lmasa kerak</p>
                </div>
            )}

            {!loading && !debouncedDachaName && !dacha && (
                <p className='initial-message'>Dacha Nomini kiritishingizni kuting.</p>
            )}
        </div>
    );
}

export default GetDachaByName;