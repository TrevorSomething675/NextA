type SuccessResponse<T> = {
  success: true;
  data: T;
  status: number;
};

type ErrorResponse<E> = {
  success: false;
  data: E;
  status: number;
};

export type ApiResponse<T, E> = SuccessResponse<T> | ErrorResponse<E>;