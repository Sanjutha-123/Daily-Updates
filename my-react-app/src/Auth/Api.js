// src/Api/auth.js
import axios from "axios";

const BASE_URL = "http://localhost:5207/api";

// Login
export const login = async (email, password) => {
  try {
    const response = await axios.post(`${BASE_URL}/Users/Login`, { email, password });
    if (response.data?.token) {
      localStorage.setItem("token", response.data.token);
     
    }
    return response.data;
  } catch (error) {
    const message = error.response?.data?.message || error.response?.statusText || "Login failed";
    throw new Error(message);
  }
  
}