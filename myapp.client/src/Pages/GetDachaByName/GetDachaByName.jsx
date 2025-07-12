import React, { useState, useEffect } from 'react';
import './GetDachaByName.css';

function GetDachaByName() {
    const [dachaName, setDachaName] = useState('');
    const [loading, setLoading] = useState(false);
    const [dacha, setDacha] = useState(null);

    useEffect(() => {
        GetData();
    }, [dachaName]);

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
                    <p>
                        <strong>ID:</strong> {dacha.id}
                    </p>
                    <p>
                        <strong>Nomi:</strong> {dacha.name}
                    </p>
                    <p>
                        <strong>Maydoni:</strong> {dacha.sqft}
                    </p>
                    <p>
                        <strong>Ijara olsa bo'ladimi?:</strong> {dacha.isAvailable ? 'Ha' : 'Yo\'q'}
                    </p>
                </div>
            )}

            {!loading && !dacha && dachaName && (
                <div className='not-found-error'>
                    <p>Berilgan Nom bo'yicha dacha topilmadi.</p>
                    <p>Bunday dacha bizda bo'lmasa kerak</p>
                </div>
            )}

            {!loading && !dachaName && !dacha && (
                <p className='initial-message'>Dacha Nomini kiritishingizni kuting.</p>
            )}
        </div>
    );

    async function GetData() {
        if (!dachaName.trim()) {
            setDacha(null);
            return;
        }

        setLoading(true);
        try {
            const encodedName = encodeURIComponent(dachaName.trim());
            const response = await fetch(`https://localhost:7019/api/MyDacha/name?name=${encodedName}`);
            
            if (response.ok) {
                const data = await response.json();
                setDacha(data);
            } else {
                setDacha(null);
            }
        } catch (error) {
            setDacha(null);
        } finally {
            setLoading(false);
        }
    }
}

export default GetDachaByName;