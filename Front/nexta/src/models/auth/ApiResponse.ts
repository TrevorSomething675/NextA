interface ApiResponse<T>{
    value: T;
    statusCode: number;
    errorMessages: string[];
}

export default ApiResponse;