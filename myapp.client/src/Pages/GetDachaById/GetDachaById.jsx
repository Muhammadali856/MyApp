import React, { useState, useEffect } from 'react';
import './GetDachaById.css';

function GetDachaById (){

    const [dachaId, setDachaId] = useState('');
    const [loading, setLoading] = useState(false);
    const [dacha, setDacha] = useState(null);

    useEffect(() => {
        GetData();
    }, [dachaId]);

    const handleInputChange = (event) => {
        setDachaId(event.target.value);
    }

    return (
        <div className='get-dacha-by-id-container'>
            <h1>Dacha haqida ma'lumot olish uchun dacha ID sini kiriting</h1>

            <input
                type="text"
                placeholder="Dacha ID kiriting..."
                value={dachaId}
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
                        <strong>Hozil olsa bo'ladimi ?:</strong> {dacha.isAvailable}
                    </p>
                </div>
            )}

            {!loading && !dacha && dachaId && (
                <div className='not-found-error'>
                    <p>Berilgan ID bo'yicha dacha topilmadi.</p>
                    <p>Bunday dacha bizda bo'lmasa kerak</p>
                </div>
            )}

            {!loading && !dachaId && !dacha && (
                <p className='initial-message'>Dacha ID kiritishingizni kuting.</p> 
            )}
        </div>
    );

    async function GetData() {
        setLoading(true);
        try {
            const response = await fetch('https://localhost:7019/api/MyDacha/' + dachaId);
            if (response.ok) {
                const data = await response.json();
                setDacha(data);
            } else {
                setDacha(); 
            }
        } catch (error) {
            setDacha(null); 
        } finally {
            setLoading(false);
        }
    }

}

export default GetDachaById
