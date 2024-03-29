import axios from "axios";
import { get_api } from "./Methods";

export async function getCategories() {
    return get_api('https://localhost:7115/api/categories');
}

export async function getFeaturedPosts(limit) {
    try {
        const response = await axios.get(`https://localhost:7115/api/posts/featured/${limit}`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        } else {
            return null;
        }
    } catch (error) {
        console.log('Error', error.message);
        return null;
    }
}

export async function getRandomPosts(limit) {
    try {
        const response = await axios.get(`https://localhost:7115/api/posts/random/${limit}`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        } else {
            return null;
        }
    } catch (error) {
        console.log('Error', error.message);
        return null;
    }
}

export async function getTags() {
    try {
        const response = await axios.get('https://localhost:7115/api/tags');
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        } else {
            return null;
        }
    } catch (error) {
        console.log('Error', error.message);
        return null;
    }
}

export async function getBestAuthors(limit) {
    try {
        const response = await axios.get(`https://localhost:7115/api/authors/best/${limit}`);
        const data = response.data;
        if (data.isSuccess) {
            return data.result;
        } else {
            return null;
        }
    } catch (error) {
        console.log('Error', error.message);
        return null;
    }
}