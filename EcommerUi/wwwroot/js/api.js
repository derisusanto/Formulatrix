const API_URL = "http://localhost:5282/api";

window.api = {
    setToken: (token) => {
        localStorage.setItem("token", token);
    },
    getToken: () => localStorage.getItem("token"),
    clearToken: () => {
        localStorage.removeItem("token");
    },

    fetch: async (endpoint, options = {}) => {
        const token = localStorage.getItem("token");
        const headers = {
            "Content-Type": "application/json",
            ...options.headers
        };

        if (token) {
            headers["Authorization"] = `Bearer ${token}`;
        }

        try {
            const response = await fetch(`${API_URL}/${endpoint}`, {
                ...options,
                headers,
                cache: 'no-store'
            });

            if (response.status === 204) return null;

            const text = await response.text();
            if (!text || text.trim() === "") return null;

            let result;
            try {
                result = JSON.parse(text);
            } catch (e) {
                if (response.ok) return text;
                throw new Error(text || `Error ${response.status}`);
            }

            // Flexible ServiceResult check (Case-insensitive)
            const keys = Object.keys(result);
            const sKey = keys.find(k => k.toLowerCase() === 'success');

            if (sKey !== undefined) {
                if (!result[sKey]) {
                    const mKey = keys.find(k => k.toLowerCase() === 'message') || 'message';
                    throw new Error(result[mKey] || "Operation failed");
                }
                const dKey = keys.find(k => k.toLowerCase() === 'data') || 'data';
                return result[dKey];
            }

            if (!response.ok) {
                const mKey = keys.find(k => k.toLowerCase() === 'message') || 'message';
                const tKey = keys.find(k => k.toLowerCase() === 'title') || 'title';
                throw new Error(result[mKey] || result[tKey] || "HTTP Error " + response.status);
            }

            return result;
        } catch (err) {
            console.error("API Fetch Error:", err);
            throw err;
        }
    },

    login: async (model) => {
        try {
            const data = await window.api.fetch("Auth/login", {
                method: "POST",
                body: JSON.stringify(model)
            });
            if (data && typeof data === 'object' && data.token) {
                window.api.setToken(data.token);
                return { success: true };
            }
            return { success: false, message: "Invalid credentials or missing token" };
        } catch (err) {
            return { success: false, message: err.message };
        }
    },

    register: async (model) => {
        try {
            const data = await window.api.fetch("Auth/register", {
                method: "POST",
                body: JSON.stringify(model)
            });
            if (data && typeof data === 'object' && data.token) {
                window.api.setToken(data.token);
                return { success: true };
            }
            return { success: false, message: "Registration failed or missing token" };
        } catch (err) {
            return { success: false, message: err.message };
        }
    }
};
