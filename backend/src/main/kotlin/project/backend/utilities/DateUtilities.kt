package project.backend.utilities

import java.util.*

object DateUtilities {
    fun addMinutes(date: Date, minutes: Int): Date {
        var dateInUnixTime: Long = date.time
        return Date(dateInUnixTime + minutes * 60 * 1000)
    }
}