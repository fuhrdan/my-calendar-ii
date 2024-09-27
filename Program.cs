//*****************************************************************************
//** 729. My Calendar II  leetcode                                           **
//*****************************************************************************


// Struct to represent a booking
typedef struct {
    int start;
    int end;
} Booking;

// MyCalendarTwo structure
typedef struct {
    Booking* singleBookings;  // Single bookings
    Booking* doubleBookings;  // Double bookings
    int singleCount;
    int doubleCount;
    int singleCapacity;
    int doubleCapacity;
} MyCalendarTwo;

// Function to create MyCalendarTwo object
MyCalendarTwo* myCalendarTwoCreate() {
    MyCalendarTwo* obj = (MyCalendarTwo*)malloc(sizeof(MyCalendarTwo));
    
    // Initialize dynamic arrays for single and double bookings
    obj->singleCapacity = 1000;
    obj->doubleCapacity = 1000;
    obj->singleBookings = (Booking*)malloc(sizeof(Booking) * obj->singleCapacity);
    obj->doubleBookings = (Booking*)malloc(sizeof(Booking) * obj->doubleCapacity);
    obj->singleCount = 0;
    obj->doubleCount = 0;
    
    return obj;
}

// Function to check if two intervals overlap
bool isOverlap(int start1, int end1, int start2, int end2) {
    return start1 < end2 && start2 < end1;
}

// Function to book an event
bool myCalendarTwoBook(MyCalendarTwo* obj, int start, int end) {
    // Check if the event would cause a triple booking
    for (int i = 0; i < obj->doubleCount; i++) {
        if (isOverlap(start, end, obj->doubleBookings[i].start, obj->doubleBookings[i].end)) {
            return false; // Triple booking detected
        }
    }

    // Check for overlaps with single bookings and add to double bookings if necessary
    for (int i = 0; i < obj->singleCount; i++) {
        if (isOverlap(start, end, obj->singleBookings[i].start, obj->singleBookings[i].end)) {
            int overlapStart = start > obj->singleBookings[i].start ? start : obj->singleBookings[i].start;
            int overlapEnd = end < obj->singleBookings[i].end ? end : obj->singleBookings[i].end;

            // Add to double bookings
            if (obj->doubleCount == obj->doubleCapacity) {
                obj->doubleCapacity *= 2;
                obj->doubleBookings = (Booking*)realloc(obj->doubleBookings, sizeof(Booking) * obj->doubleCapacity);
            }
            obj->doubleBookings[obj->doubleCount++] = (Booking){overlapStart, overlapEnd};
        }
    }

    // Add the new event to single bookings
    if (obj->singleCount == obj->singleCapacity) {
        obj->singleCapacity *= 2;
        obj->singleBookings = (Booking*)realloc(obj->singleBookings, sizeof(Booking) * obj->singleCapacity);
    }
    obj->singleBookings[obj->singleCount++] = (Booking){start, end};

    return true; // Event booked successfully
}

// Function to free the MyCalendarTwo object
void myCalendarTwoFree(MyCalendarTwo* obj) {
    free(obj->singleBookings);
    free(obj->doubleBookings);
    free(obj);
}