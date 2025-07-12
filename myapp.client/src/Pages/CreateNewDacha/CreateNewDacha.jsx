import React, { useState } from 'react';
import './CreateNewDacha.css';

const CreateNewDacha = () => {
    const [loading, setLoading] = useState(false);
    const [name, setName] = useState("");
    const [sqft, setSqft] = useState("");
    const [isAvailable, setIsAvailable] = useState("Ha");

    const handleSubmit = async (event) => {
        event.preventDefault();
        await postDachaData();
    };

    async function postDachaData() {
        setLoading(true);

        try {
            const response = await fetch('https://localhost:7019/api/MyDacha', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    name: name,
                    sqft: parseInt(sqft, 10),
                    isAvailable: isAvailable
                })
            });

            if (response.ok) {
                alert('Dacha created successfully! 🎉');
                setName("");
                setSqft("");
                setIsAvailable("Ha");
            } else {
                const errorData = await response.json();
                console.error('Error response from server:', errorData);
                alert(`Failed to create Dacha: ${errorData.message || errorData.CustomError || response.statusText}`);
            }
        } catch (error) {
            console.error('Network or parsing error:', error);
            alert(`An error occurred: ${error.message}. Please check your connection or server.`);
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className="create-dacha-container">
            <h1>Create New Dacha 🏡</h1>
            {loading ? (
                <p className="loading-message">Loading... Creating Dacha...</p>
            ) : (
                <form onSubmit={handleSubmit}>
                    <div>
                        <label htmlFor="dachaName">Dacha Name:</label>
                        <input
                            type="text"
                            id="dachaName"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                            disabled={loading}
                        />
                    </div>
                    <div>
                        <label htmlFor="dachaSqft">Square Footage (sqft):</label>
                        <input
                            type="number"
                            id="dachaSqft"
                            value={sqft}
                            onChange={(e) => setSqft(e.target.value)}
                            required
                            disabled={loading}
                        />
                    </div>
                    <div>
                        <label htmlFor="dachaAvailable">Is Available:</label>
                        <select
                            id="dachaAvailable"
                            value={isAvailable}
                            onChange={(e) => setIsAvailable(e.target.value)}
                            disabled={loading}
                        >
                            <option value="Ha">Ha (Yes)</option>
                            <option value="Yo'q">Yo'q (No)</option>
                        </select>
                    </div>
                    <button type="submit" disabled={loading}>
                        {loading ? 'Creating...' : 'Create Dacha'}
                    </button>
                </form>
            )}
        </div>
    );
}

export default CreateNewDacha;