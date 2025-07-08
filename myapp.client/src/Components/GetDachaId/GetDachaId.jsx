import React, { useState, useEffect } from 'react';
import './GetDachaId.css';

function GetDachaId (){

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
        <div className='body'>
            <h1>Dacha haqida ma'lumot olish uchun dacha Id sini kiriting</h1>

            <input
                type="text"
                placeholder="Dacha ID kiriting..."
                value={dachaId}
                onChange={handleInputChange}
            />

            {loading && <div>Yuklanmoqda...</div>}

            {!loading && dacha && (
                <div className='dacha-details'>
                    <h2>Dacha ma'lumotlari:</h2>
                    <p>
                        <strong>ID:</strong> {dacha.id}
                    </p>
                    <p>
                        <strong>Nomi:</strong> {dacha.name}
                    </p>
                </div>
            )}

            {!loading && !dacha && dachaId && (
                <div className='notFinded-Error'>
                    <p>Berilgan ID bo'yicha dacha topilmadi.</p>
                    <p>Bunday dacha bizda bo'lmasa kerak</p>
                </div>
            )}

            {!loading && !dachaId && !dacha && (
                <p>Dacha ID kiritishingizni kuting.</p>
            )}
        </div>
    );

    async function GetData() {
        setLoading(true);
        try {
            const response = await fetch('https://localhost:7019/api/MyDacha/id?id=' + dachaId);
            if (response.ok) {
                const data = await response.json();
                console.log('Fetched data:', data);
                setDacha(data);
            } else {
                console.error('Failed to fetch dachas:', response.status, response.statusText);
                setDacha(); 
            }
        } catch (error) {
            console.error('Network or other error during fetch:', error);
            setDacha(null); 
        } finally {
            setLoading(false);
        }
    }

}

export default GetDachaId