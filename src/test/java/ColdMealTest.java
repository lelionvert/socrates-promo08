import domain.CheckIn;
import domain.CheckInHour;
import domain.CheckInParser;
import domain.ColdMealChecker;
import org.junit.Test;

import java.time.DayOfWeek;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;

public class ColdMealTest {

    @Test
    public void should_return_cold_meals_between_21_and_midnight() {
        List<CheckIn> coldMealCheckins = Arrays.asList(
                new CheckIn(DayOfWeek.THURSDAY, new CheckInHour("22h00")),
                new CheckIn(DayOfWeek.THURSDAY, new CheckInHour("23h59")),
                new CheckIn(DayOfWeek.THURSDAY, new CheckInHour("21h00"))
        );
        assertThat(ColdMealChecker.count(coldMealCheckins)).isEqualTo(3);
    }

    @Test
    public void should_return_no_cold_meal_before_21() {
        List<CheckIn> hotMealCheckins = Arrays.asList(
                new CheckIn(DayOfWeek.THURSDAY, new CheckInHour("19h00")),
                new CheckIn(DayOfWeek.THURSDAY, new CheckInHour("17h00")),
                new CheckIn(DayOfWeek.THURSDAY, new CheckInHour("20h59"))
        );
        assertThat(ColdMealChecker.count(hotMealCheckins)).isEqualTo(0);
    }

    @Test
    public void should_return_no_cold_meal_after_00() {
        List<CheckIn> lateMealCheckins = Arrays.asList(
                new CheckIn(DayOfWeek.FRIDAY, new CheckInHour("01h00")),
                new CheckIn(DayOfWeek.FRIDAY, new CheckInHour("00h00")),
                new CheckIn(DayOfWeek.SATURDAY, new CheckInHour("23h00"))
        );
        assertThat(ColdMealChecker.count(lateMealCheckins)).isEqualTo(0);
    }

    @Test
    public void should_return_number_of_cold_meal_with_parsing() {
        List<CheckIn> lateMealCheckins = Arrays.asList(
                CheckInParser.parse("thursday 21h00"),
                CheckInParser.parse("Thursday 23h59"),
                CheckInParser.parse("Thursday 19h00"),
                CheckInParser.parse("FRIDAY 00h00"),
                CheckInParser.parse("Thursday 22h")
        );
        assertThat(ColdMealChecker.count(lateMealCheckins)).isEqualTo(3);
    }

    @Test
    public void should_return_no_cold_meal_if_empty() {
        List<CheckIn> lateMealCheckins = Collections.singletonList(
                CheckInParser.parse("")
        );
        assertThat(ColdMealChecker.count(lateMealCheckins)).isEqualTo(0);
    }


}
