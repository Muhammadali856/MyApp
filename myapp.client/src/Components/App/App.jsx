import { useState, useEffect } from 'react';
import './App.css';

const App = () => {
    const [dachas, setDachas] = useState([]); 
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        GetData();
    }, []);

    return (
        <div className='body'>
            {loading && <div>Loading...</div>}
            {!loading && (
                <>
                    <h1>Available Dachas:</h1>
                    {dachas && dachas.length > 0 ? (
                        <ol>
                            {dachas.map(dacha => (
                                <li key={dacha.Id}>{dacha.name}</li>
                            ))}
                        </ol>
                    ) : (
                        !loading && <p>No dachas available.</p>
                    )}
                </>
            )}
        </div>
    );

    async function GetData() {
        setLoading(true);
        try {
            const response = await fetch('https://localhost:7019/api/MyDacha');
            if (response.ok) {
                const data = await response.json();
                console.log('Fetched data:', data);
                setDachas(data);
            } else {
                console.error('Failed to fetch dachas:', response.status, response.statusText);
                setDachas([]); 
            }
        } catch (error) {
            console.error('Network or other error during fetch:', error);
            setDachas([]); 
        } finally {
            setLoading(false);
        }
    }
};

export default App;