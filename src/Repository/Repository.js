import axios from "axios";

const baseDomain = "http://localhost:5041";
const baseUrl = `${baseDomain}/api/v1`;

export default axios.create({
    baseURL: baseUrl,
    header: {"Authorization": "Bearer yourToken"}
})