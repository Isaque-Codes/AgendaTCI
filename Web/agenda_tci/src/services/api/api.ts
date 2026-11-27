import axios from "axios";

const apiCaminho = process.env.NEXT_PUBLIC_API_URL;

export const api = axios.create({
    baseURL: apiCaminho
})